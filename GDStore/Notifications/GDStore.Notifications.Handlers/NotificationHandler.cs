using System.Threading.Tasks;
using GDStore.Notifications.Messages.Commands;
using GDStore.Notifications.Services;
using log4net;
using MassTransit;

namespace GDStore.Notifications.Handlers
{
    public class NotificationHandler : IConsumer<SendEmailCommand>
    {
        private readonly INotificationService notificationService;
        private static readonly ILog log = LogManager.GetLogger(typeof(NotificationHandler));
        
        public NotificationHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            log.Info($"{nameof(SendEmailCommand)} handler called");

            await notificationService.SendEmailAsync(context.Message);
        }
    }
}
