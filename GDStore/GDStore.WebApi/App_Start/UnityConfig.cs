using System;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Context;
using GDStore.DAL.SQL.Services;
using GDStore.WebApi.Services;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using System.Configuration;
using GDStore.WebApi.CommandBus;
using MassTransit;

namespace GDStore.WebApi
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
            var gdStoreContext = new GDStoreContext();

            container.RegisterType<ICustomerRepository, CustomerRepository>(new InjectionConstructor(gdStoreContext));
            container.RegisterType<ISuitRepository, SuitRepository>(new InjectionConstructor(gdStoreContext));
            container.RegisterType<IAlterationRepository, AlterationRepository>(new InjectionConstructor(gdStoreContext));
            container.RegisterType<IAlterationService, AlterationService>(new PerRequestLifetimeManager());

            var rabbitMqUri = ConfigurationManager.ConnectionStrings["GDStore.RabbitMq.ConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(rabbitMqUri))
            {
                throw new ConfigurationErrorsException("RabbitMq connectionString is empty");
            }

            //var queueName = ConfigurationManager.AppSettings["GDStore.Alterations.RabbitMQ.QueueName"];
            //if (string.IsNullOrEmpty(queueName))
            //{
            //    throw new ConfigurationErrorsException("GDStore.Alterations.RabbitMQ.QueueName is empty");
            //}

            var queueURI = ConfigurationManager.AppSettings["GDStore.Alterations.RabbitMQ.QueueURI"];
            if (string.IsNullOrEmpty(queueURI))
            {
                throw new ConfigurationErrorsException("GDStore.Alterations.RabbitMQ is empty");
            }

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(rabbitMqUri), h => { });
                //cfg.UseLog4Net();
            });

            //Command buses 
            container.RegisterInstance(bus);
            container.RegisterType<IAlterationsCommandBus, AlterationsCommandBus>(
                new PerRequestLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IBusControl>(), new Uri(queueURI)));
        }
    }
}