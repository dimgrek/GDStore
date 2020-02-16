using System.Threading.Tasks;
using GDStore.Payments.Messages.Commands;

namespace GDStore.Payments.Services
{
    public interface IPaymentsService
    {
        Task HandlePayment(PaymentDoneCommand command);
    }
}