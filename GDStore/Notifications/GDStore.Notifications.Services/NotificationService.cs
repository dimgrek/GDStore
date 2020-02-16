using System.Threading.Tasks;
using GDStore.Notifications.Messages.Commands;
using log4net;

namespace GDStore.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NotificationService));

        public async Task SendEmailAsync(SendEmailCommand command)
        {
            log.Info($"{nameof(SendEmailAsync)} called");

            //install email service e.g. Send grid and use it to send email to particular customer

            log.Info($"Email sent out to {command.Email}");
        }
    }
}
