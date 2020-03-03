using System.Threading.Tasks;
using GDStore.DAL.Interface.Services;
using GDStore.Notifications.Messages.Commands;
using log4net;

namespace GDStore.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAlterationRepository alterationRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ISuitRepository suitRepository;
        private static readonly ILog log = LogManager.GetLogger(typeof(NotificationService));

        public NotificationService(IAlterationRepository alterationRepository, 
            ICustomerRepository customerRepository, 
            ISuitRepository suitRepository)
        {
            this.alterationRepository = alterationRepository;
            this.customerRepository = customerRepository;
            this.suitRepository = suitRepository;
        }

        public async Task SendEmailAsync(SendEmailCommand command)
        {
            log.Info($"{nameof(SendEmailAsync)} called");

            var alteration = await alterationRepository.GetByIdAsync(command.AlterationId);
            var suit = await suitRepository.GetByIdAsync(alteration.SuitId);
            var customer = await customerRepository.GetByIdAsync(suit.CustomerId);

            //install email service e.g. SendGrid and use it to send email to particular customer
            
            log.Info($"Email sent to {customer.Email}");
        }
    }
}
