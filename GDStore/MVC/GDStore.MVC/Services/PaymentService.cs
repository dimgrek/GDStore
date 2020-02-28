using System.Threading.Tasks;
using GDStore.Alterations.Messages.Events;
using GDStore.MVC.Models;
using log4net;
using MassTransit;

namespace GDStore.MVC.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILog log;
        private readonly IBusControl bus;

        public PaymentService(ILog log, IBusControl bus)
        {
            this.log = log;
            this.bus = bus;
        }

        public async Task MakePayment(PaymentModel model)
        {
            log.Info($"{nameof(MakePayment)} called");

            await bus.Publish(new MakePaymentEvent { AlterationId = model.AlterationId });
        }
    }
}