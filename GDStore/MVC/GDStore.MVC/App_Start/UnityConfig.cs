using System;
using System.Configuration;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Messages.Requests;
using GDStore.Alterations.Messages.Responses;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Context;
using GDStore.DAL.SQL.Services;
using GDStore.MVC.CommandBus;
using GDStore.MVC.Services;
using MassTransit;
using MassTransit.Log4NetIntegration;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using Unity.log4net;

namespace GDStore.MVC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.AddNewExtension<Log4NetExtension>();
            container.RegisterType<GDStoreContext>(new TransientLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new TransientLifetimeManager());
            container.RegisterType<ISuitRepository, SuitRepository>(new TransientLifetimeManager());
            container.RegisterType<ISuitService, SuitService>(new TransientLifetimeManager());
            container.RegisterType<IAlterationRepository, AlterationRepository>(new TransientLifetimeManager());
            container.RegisterType<IAlterationService, AlterationService>(new TransientLifetimeManager());
            container.RegisterType<IPaymentService, PaymentService>(new TransientLifetimeManager());

            var rabbitMqUri = ConfigurationManager.ConnectionStrings["GDStore.RabbitMq.ConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(rabbitMqUri))
            {
                throw new ConfigurationErrorsException("RabbitMq connectionString is empty");
            }

            var alterationsQueue = ConfigurationManager.AppSettings["GDStore.Alterations.RabbitMQ.QueueURI"];
            if (string.IsNullOrEmpty(alterationsQueue))
            {
                throw new ConfigurationErrorsException("GDStore.Alterations.RabbitMQ is empty");
            }
            
            if (!int.TryParse(ConfigurationManager.AppSettings["GDStore.RabbitMQ.Timeout"], out int rabbitMqTimeout))
                throw new ConfigurationErrorsException("GDStore.RabbitMQ.Timeout is empty or invalid");

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(rabbitMqUri), h => { });
                cfg.UseLog4Net();
            });

            container.RegisterInstance(bus);
            
            //Request response clients
            container.RegisterInstance(bus.CreateRequestClient<AddAlterationRequest, AddAlterationResponse>(
                new Uri(alterationsQueue), 
                TimeSpan.FromSeconds(rabbitMqTimeout)));

            bus.Start();
        }
    }
}