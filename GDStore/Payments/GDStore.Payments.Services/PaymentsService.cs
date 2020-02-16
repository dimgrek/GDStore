using System.Threading.Tasks;
using GDStore.Payments.Messages.Commands;

namespace GDStore.Payments.Services
{
    public class PaymentsService : IPaymentsService
    {
        public async Task HandlePayment(PaymentDoneCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
