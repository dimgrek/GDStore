using System.Threading.Tasks;
using GDStore.WebApi.Models;

namespace GDStore.WebApi.Services
{
    public interface IPaymentService
    {
        Task MakePayment(PaymentModel model);
    }
}