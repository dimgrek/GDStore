using System.Threading.Tasks;
using System.Web.Mvc;
using GDStore.WebApi.Models;
using GDStore.WebApi.Services;

namespace GDStore.WebApi.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        //[HttpPost]
        public async Task<ActionResult> Make([Bind(Include = "AlterationId,CustomerId")] PaymentModel model)
        {
            await paymentService.MakePayment(model);
            return RedirectToAction("AlterationsByCustomerId", "Alteration", new { model.CustomerId });
        }
    }
}