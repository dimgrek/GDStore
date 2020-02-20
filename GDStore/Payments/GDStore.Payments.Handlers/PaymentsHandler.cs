using System.Threading.Tasks;
using GDStore.Payments.Messages.Commands;
using GDStore.Payments.Services.Services;
using MassTransit;

namespace GDStore.Payments.Handlers
{
    public class PaymentsHandler : IConsumer<PaymentDoneCommand>
    {
        private readonly IPaymentsService paymentsService;

        public PaymentsHandler(IPaymentsService paymentsService)
        {
            this.paymentsService = paymentsService;
        }

        public async Task Consume(ConsumeContext<PaymentDoneCommand> context)
        {
            await paymentsService.HandlePayment(context.Message);
        }
    }
}
