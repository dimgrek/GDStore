using System.Threading.Tasks;
using GDStore.Payments.Messages.Commands;
using GDStore.WebApi.CommandBus;
using GDStore.WebApi.Models;

namespace GDStore.WebApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentsCommandBus paymentsCommandBus;

        public PaymentService(IPaymentsCommandBus paymentsCommandBus)
        {
            this.paymentsCommandBus = paymentsCommandBus;
        }

        public async Task MakePayment(PaymentModel model)
        {
            await paymentsCommandBus.SendAsync(new PaymentDoneCommand
            {
                AlterationId = model.AlterationId,
                CustomerId = model.CustomerId
            });
        }
    }
}