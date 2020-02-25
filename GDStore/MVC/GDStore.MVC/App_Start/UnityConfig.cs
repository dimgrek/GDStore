using System;
using System.Configuration;
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
            container.RegisterType<GDStoreContext>();
            container.RegisterType<ICustomerRepository, CustomerRepository>(new PerRequestLifetimeManager());
            container.RegisterType<ISuitRepository, SuitRepository>(new PerRequestLifetimeManager());
            container.RegisterType<ISuitService, SuitService>(new PerRequestLifetimeManager());
            container.RegisterType<IAlterationRepository, AlterationRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IAlterationService, AlterationService>(new PerRequestLifetimeManager());
            container.RegisterType<IPaymentService, PaymentService>(new PerRequestLifetimeManager());

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

            var paymentsQueue = ConfigurationManager.AppSettings["GDStore.Payments.RabbitMQ.QueueURI"];
            if (string.IsNullOrEmpty(alterationsQueue))
            {
                throw new ConfigurationErrorsException("GDStore.Payments.RabbitMQ is empty");
            }

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(rabbitMqUri), h => { });
                cfg.UseLog4Net();
            });

            //Command buses 
            container.RegisterInstance(bus);
            container.RegisterType<IAlterationsCommandBus, AlterationsCommandBus>(
                new PerRequestLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IBusControl>(), new Uri(alterationsQueue)));

            container.RegisterType<IPaymentsCommandBus, PaymentsCommandBus>(
                new PerRequestLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IBusControl>(), new Uri(paymentsQueue)));
        }
    }
}