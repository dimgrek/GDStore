using System.Threading.Tasks;
using GDStore.MVC.Models;

namespace GDStore.MVC.Services
{
    public interface IPaymentService
    {
        Task MakePayment(PaymentModel model);
    }
}