using System.Configuration;

namespace GDStore.Notifications.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitMqUri = ConfigurationManager.ConnectionStrings["GDStore.RabbitMq.ConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(rabbitMqUri))
            {
                throw new ConfigurationErrorsException("RabbitMq connectionString is empty");
            }

            var queueName = ConfigurationManager.AppSettings["GDStore.Notifications.RabbitMQ.QueueName"];
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ConfigurationErrorsException("GDStore.Notifications.RabbitMQ.QueueName is empty");
            }

            var service = new ConfigurationService(rabbitMqUri, queueName);
            service.Start();
        }
    }
}
