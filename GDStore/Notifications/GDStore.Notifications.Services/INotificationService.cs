using System.Threading.Tasks;
using GDStore.Notifications.Messages.Commands;

namespace GDStore.Notifications.Services
{
    public interface INotificationService
    {
        Task SendEmailAsync(SendEmailCommand command);
    }
}