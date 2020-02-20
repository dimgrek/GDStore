using System;
using System.Configuration;
using GDStore.Alterations.Handlers;
using GDStore.Alterations.Services;
using GDStore.Alterations.Services.CommandBus;
using GDStore.Alterations.Services.Services;
using GDStore.BLL.Services.Observers;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Context;
using GDStore.DAL.SQL.Services;
using log4net;
using MassTransit;
using MassTransit.Log4NetIntegration;
using MassTransit.RabbitMqTransport;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace GDStore.Alterations.Console
{
    public class ConfigurationService
    {
        private readonly string rabbitMqUri;
        private readonly string queueName;
        private UnityContainer container;
        private IBusControl bus;
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationService));

        public ConfigurationService(string rabbitMqUri, string queueName)
        {
            this.rabbitMqUri = rabbitMqUri;
            this.queueName = queueName;
        }

        public bool Start()
        {
            log.Info("Initializing services...");
            container = new UnityContainer();
            container.RegisterType<GDStoreContext>();
            container.RegisterType<ISuitRepository, SuitRepository>(new TransientLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new TransientLifetimeManager());
            container.RegisterType<IAlterationRepository, AlterationRepository>(new TransientLifetimeManager());
            container.RegisterType<IAlterationService, AlterationService>(new TransientLifetimeManager());

            RabbitMQConfiguration();
            log.Info("Console running...");

            return true;
        }

        private void RabbitMQConfiguration()
        {
            log.Info("Registering rabbitmq...");

            //Handlers
            container.RegisterType<AlterationsHandler>(new TransientLifetimeManager());

            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(rabbitMqUri), h => { });

                cfg.ReceiveEndpoint(host, queueName, endPoint => { endPoint.LoadFrom(container); });
                cfg.UseLog4Net();
            });

            container.RegisterInstance(bus);

            var alterationsQueue = ConfigurationManager.AppSettings["GDStore.Notifications.RabbitMQ.QueueURI"];
            if (string.IsNullOrEmpty(alterationsQueue))
            {
                throw new ConfigurationErrorsException("GDStore.Alterations.RabbitMQ is empty");
            }

            container.RegisterType<INotificationCommandBus, NotificationCommandBus>(
                new TransientLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IBusControl>(), new Uri(alterationsQueue)));

            bus.ConnectConsumeObserver(new MessagesConsumerObserver());

            try
            {
                bus.Start();
            }
            catch (RabbitMqConnectionException ex)
            {
                log.Error(ex);
            }
        }
    }
}