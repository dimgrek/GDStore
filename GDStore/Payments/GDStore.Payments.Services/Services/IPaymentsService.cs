using System.Threading.Tasks;
using GDStore.Payments.Messages.Commands;

namespace GDStore.Payments.Services.Services
{
    public interface IPaymentsService
    {
        Task HandlePayment(MakePaymentCommand command);
    }
}