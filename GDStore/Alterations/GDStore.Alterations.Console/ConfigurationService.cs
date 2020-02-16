using System;
using GDStore.Alterations.Handlers;
using GDStore.Alterations.Services;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Services;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Unity;
using Unity.Lifetime;

namespace GDStore.Alterations.Console
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

            container.RegisterType<ISuitRepository, SuitRepository>(new TransientLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new TransientLifetimeManager());
            container.RegisterType<IAlterationRepository, AlterationRepository>(new TransientLifetimeManager());
            container.RegisterType<IAlterationService, AlterationService>(new TransientLifetimeManager());

            RabbitMQConfiguration();
            return true;
        }

        private void RabbitMQConfiguration()
        {
            //Handlers
            container.RegisterType<AlterationsHandler>(new TransientLifetimeManager());

            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(rabbitMqUri), h => { });

                cfg.ReceiveEndpoint(host, queueName, endPoint => { endPoint.LoadFrom(container); });
                //cfg.UseLog4Net();
            });

            container.RegisterInstance(bus);

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