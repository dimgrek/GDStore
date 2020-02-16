using System.Threading.Tasks;
using GDStore.Notifications.Messages.Commands;

namespace GDStore.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        public async Task SendEmailAsync(SendEmailCommand command)
        {
            //install email service e.g. Send grid and use it to send email to particular customer

        }
    }
}
