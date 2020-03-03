using System;
using GDStore.BLL.Services.Observers;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Context;
using GDStore.DAL.SQL.Services;
using GDStore.Notifications.Handlers;
using GDStore.Notifications.Services;
using log4net;
using MassTransit;
using MassTransit.Log4NetIntegration;
using MassTransit.RabbitMqTransport;
using Unity;
using Unity.Lifetime;

namespace GDStore.Notifications.Console
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
            container.RegisterType<IAlterationRepository, AlterationRepository>(new TransientLifetimeManager());
            container.RegisterType<ISuitRepository, SuitRepository>(new TransientLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new TransientLifetimeManager());
            container.RegisterType<INotificationService, NotificationService>(new TransientLifetimeManager());

            RabbitMQConfiguration();
            log.Info("Console running...");

            return true;
        }

        private void RabbitMQConfiguration()
        {
            log.Info("Registering rabbitmq...");

            //Handlers
            container.RegisterType<NotificationHandler>(new TransientLifetimeManager());

            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(rabbitMqUri), h => { });

                cfg.ReceiveEndpoint(host, queueName, endPoint => { endPoint.LoadFrom(container); });
                cfg.UseLog4Net();
            });

            container.RegisterInstance(bus);

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