using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GDStore.MVC.Services;

namespace GDStore.MVC.Controllers
{
    public class SuitController : Controller
    {
        private readonly ISuitService suitService;

        public SuitController(ISuitService suitService)
        {
            this.suitService = suitService;
        }
        // GET
        public async Task<ActionResult> Index(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            return View(await suitService.GetAllByCustomerId(customerId));
        }
    }
}