using System.Threading.Tasks;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.Payments.Messages.Commands;
using log4net;

namespace GDStore.Payments.Services.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IAlterationRepository alterationRepository;
        private static readonly ILog log = LogManager.GetLogger(typeof(PaymentsService));

        public PaymentsService(IAlterationRepository alterationRepository)
        {
            this.alterationRepository = alterationRepository;
        }

        public async Task HandlePayment(MakePaymentCommand command)
        {
            log.Info($"{nameof(HandlePayment)} called");

            var alteration = await alterationRepository.GetByIdAsync(command.AlterationId);
            if (alteration != null)
            {
                alteration.Status = AlterationStatus.Paid;
                await alterationRepository.SaveChangesAsync();
            }
        }
    }
}
