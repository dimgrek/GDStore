﻿using System;
using GDStore.Notifications.Handlers;
using GDStore.Notifications.Services;
using MassTransit;
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

        public ConfigurationService(string rabbitMqUri, string queueName)
        {
            this.rabbitMqUri = rabbitMqUri;
            this.queueName = queueName;
        }

        public bool Start()
        {
            container = new UnityContainer();

            container.RegisterType<INotificationService, NotificationService>(new TransientLifetimeManager());

            RabbitMQConfiguration();
            return true;
        }

        private void RabbitMQConfiguration()
        {
            //Handlers
            container.RegisterType<NotificationHandler>(new TransientLifetimeManager());

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