using System.Threading.Tasks;
using GDStore.Payments.Messages.Commands;
using GDStore.WebApi.CommandBus;
using GDStore.WebApi.Models;
using log4net;

namespace GDStore.WebApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentsCommandBus paymentsCommandBus;
        private readonly ILog log = LogManager.GetLogger(typeof(PaymentService));

        public PaymentService(IPaymentsCommandBus paymentsCommandBus)
        {
            this.paymentsCommandBus = paymentsCommandBus;
        }

        public async Task MakePayment(PaymentModel model)
        {
            log.Info($"{nameof(MakePayment)} called");

            await paymentsCommandBus.SendAsync(new PaymentDoneCommand
            {
                AlterationId = model.AlterationId,
                CustomerId = model.CustomerId
            });
        }
    }
}