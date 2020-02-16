using System.Threading.Tasks;
using GDStore.Payments.Messages.Commands;
using GDStore.Payments.Services.Services;
using log4net;
using MassTransit;

namespace GDStore.Payments.Handlers
{
    public class PaymentsHandler : IConsumer<PaymentDoneCommand>
    {
        private readonly IPaymentsService paymentsService;
        private static readonly ILog log = LogManager.GetLogger(typeof(PaymentsHandler));

        public PaymentsHandler(IPaymentsService paymentsService)
        {
            this.paymentsService = paymentsService;
        }

        public async Task Consume(ConsumeContext<PaymentDoneCommand> context)
        {
            log.Info($"{nameof(PaymentDoneCommand)} handler called");

            await paymentsService.HandlePayment(context.Message);
        }
    }
}
