using System.Threading.Tasks;
using GDStore.MVC.CommandBus;
using GDStore.MVC.Models;
using GDStore.Payments.Messages.Commands;
using log4net;

namespace GDStore.MVC.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentsCommandBus paymentsCommandBus;
        private readonly ILog log;

        public PaymentService(IPaymentsCommandBus paymentsCommandBus, ILog log)
        {
            this.paymentsCommandBus = paymentsCommandBus;
            this.log = log;
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