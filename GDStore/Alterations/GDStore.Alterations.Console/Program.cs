using System.Configuration;

namespace GDStore.Alterations.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            var queueURI = ConfigurationManager.AppSettings["GDStore.Alterations.RabbitMQ.QueueURI"];
            if (string.IsNullOrEmpty(queueURI))
            {
                throw new ConfigurationErrorsException("GDStore.Alterations.RabbitMQ is empty");
            }
            var service = new ConfigurationService(rabbitMqUri, queueName, queueURI);

        }
    }
}
