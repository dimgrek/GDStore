using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GDStore.MVC.Models;
using GDStore.MVC.Services;

namespace GDStore.MVC.Controllers
{
    public class AlterationController : Controller
    {
        private readonly IAlterationService alterationService;

        public AlterationController(IAlterationService alterationService)
        {
            this.alterationService = alterationService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await alterationService.GetAll());
        }

        public async Task<ActionResult> AlterationsByCustomerId(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            return View(await alterationService.GetAllByCustomerId(customerId));
        }

        public ActionResult Add(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "CustomerId,Name,Item,Side,Length")] AlterationModel model)
        {
            await alterationService.AddAlteration(model);
            return RedirectToAction(nameof(AlterationsByCustomerId), new {model.CustomerId});
        }
    }
}