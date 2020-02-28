using System.Threading.Tasks;
using System.Web.Mvc;
using GDStore.MVC.Models;
using GDStore.MVC.Services;

namespace GDStore.MVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public async Task<ActionResult> Make([Bind(Include = "AlterationId,SuitId")] PaymentModel model)
        {
            await paymentService.MakePayment(model);
            return RedirectToAction("AlterationsBySuitId", "Alteration", new { model.SuitId });
        }
    }
}