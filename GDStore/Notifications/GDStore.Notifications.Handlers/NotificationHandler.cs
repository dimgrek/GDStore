using System;
using System.Threading.Tasks;
using GDStore.Notifications.Messages.Commands;
using GDStore.Notifications.Services;
using MassTransit;

namespace GDStore.Notifications.Handlers
{
    public class NotificationHandler : IConsumer<SendEmailCommand>
    {
        private readonly INotificationService notificationService;

        public NotificationHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            await notificationService.SendEmailAsync(context.Message);
        }
    }
}
