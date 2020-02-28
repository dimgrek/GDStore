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

            //var alteration = await alterationRepository.GetByIdAsync(command.AlterationId);
            //var customer = await customerRepository.GetByIdAsync(alteration.CustomerId);
            //install email service e.g. SendGrid and use it to send email to particular customer
            //todo: figure out coupling between customer and alteration
            
            log.Info($"Email sent");
        }
    }
}
