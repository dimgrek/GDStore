using System.Configuration;
using log4net;
using log4net.Config;

namespace GDStore.Alterations.Console
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            log.Info("Starting...");

            var rabbitMqUri = ConfigurationManager.ConnectionStrings["GDStore.RabbitMq.ConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(rabbitMqUri))
            {
                throw new ConfigurationErrorsException("RabbitMq connectionString is empty");
            }

            var queueName = ConfigurationManager.AppSettings["GDStore.Alterations.RabbitMQ.QueueName"];
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ConfigurationErrorsException("GDStore.Alterations.RabbitMQ.QueueName is empty");
            }

            var service = new ConfigurationService(rabbitMqUri, queueName);
            service.Start();
        }
    }
}
