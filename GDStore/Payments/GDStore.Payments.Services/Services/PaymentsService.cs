using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.Payments.Messages.Commands;
using GDStore.Payments.Services.CommandBus;

namespace GDStore.Payments.Services.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IAlterationsCommandBus alterationsCommandBus;
        private readonly IAlterationRepository alterationRepository;

        public PaymentsService(IAlterationsCommandBus alterationsCommandBus, IAlterationRepository alterationRepository)
        {
            this.alterationsCommandBus = alterationsCommandBus;
            this.alterationRepository = alterationRepository;
        }

        public async Task HandlePayment(PaymentDoneCommand command)
        {
            var alteration = await alterationRepository.GetByIdAsync(command.AlterationId);
            if (alteration != null)
            {
                alteration.Status = AlterationStatus.Paid;
                await alterationRepository.SaveChangesAsync();

                await alterationsCommandBus.SendAsync(new MakeAlterationCommand
                {
                    AlterationId = command.AlterationId,
                    CustomerId = command.CustomerId
                });
            }
        }
    }
}
