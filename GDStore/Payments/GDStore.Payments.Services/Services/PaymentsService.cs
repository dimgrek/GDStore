using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.Payments.Messages.Commands;
using GDStore.Payments.Services.CommandBus;
using log4net;

namespace GDStore.Payments.Services.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IAlterationsCommandBus alterationsCommandBus;
        private readonly IAlterationRepository alterationRepository;
        private static readonly ILog log = LogManager.GetLogger(typeof(PaymentsService));


        public PaymentsService(IAlterationsCommandBus alterationsCommandBus, IAlterationRepository alterationRepository)
        {
            this.alterationsCommandBus = alterationsCommandBus;
            this.alterationRepository = alterationRepository;
        }

        public async Task HandlePayment(PaymentDoneCommand command)
        {
            log.Info($"{nameof(HandlePayment)} called");

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
