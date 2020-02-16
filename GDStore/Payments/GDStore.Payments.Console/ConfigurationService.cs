using System;
using System.Configuration;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Services;
using GDStore.Payments.Handlers;
using GDStore.Payments.Services;
using GDStore.Payments.Services.CommandBus;
using GDStore.Payments.Services.Services;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace GDStore.Payments.Console
{
    public class ConfigurationService
    {
        private readonly string rabbitMqUri;
        private readonly string queueName;
        private UnityContainer container;
        private IBusControl bus;

        public ConfigurationService(string rabbitMqUri, string queueName)
        {
            this.rabbitMqUri = rabbitMqUri;
            this.queueName = queueName;
        }

        public bool Start()
        {
            container = new UnityContainer();

            container.RegisterType<IAlterationRepository, AlterationRepository>(new TransientLifetimeManager());
            container.RegisterType<IPaymentsService, PaymentsService>(new TransientLifetimeManager());

            RabbitMQConfiguration();
            return true;
        }

        private void RabbitMQConfiguration()
        {
            //Handlers
            container.RegisterType<PaymentsHandler>(new TransientLifetimeManager());

            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(rabbitMqUri), h => { });

                cfg.ReceiveEndpoint(host, queueName, endPoint => { endPoint.LoadFrom(container); });
                //cfg.UseLog4Net();
            });

            container.RegisterInstance(bus);

            var alterationsQueue = ConfigurationManager.AppSettings["GDStore.Alterations.RabbitMQ.QueueURI"];
            if (string.IsNullOrEmpty(alterationsQueue))
            {
                throw new ConfigurationErrorsException("GDStore.Alterations.RabbitMQ is empty");
            }

            container.RegisterType<IAlterationsCommandBus, AlterationsCommandBus>(
                new TransientLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IBusControl>(), new Uri(alterationsQueue)));

            try
            {
                bus.Start();
            }
            catch (RabbitMqConnectionException ex)
            {
                //log.Error(ex);
            }
        }
    }
}