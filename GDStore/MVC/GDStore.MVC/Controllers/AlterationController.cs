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

        public async Task<ActionResult> AlterationsBySuitId(Guid suitId)
        {
            ViewBag.SuitId = suitId;
            return View(await alterationService.GetAllBySuitId(suitId));
        }

        public ActionResult Add(Guid suitId)
        {
            ViewBag.SuitId = suitId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "SuitId,Name,Item,Side,Length")] AlterationModel model)
        {
            var alteration = await alterationService.AddAlteration(model);
            if (alteration != null)
            {
                return RedirectToAction(nameof(AlterationsBySuitId), new { model.SuitId});
            }

            return RedirectToAction(nameof(Index));

            //return FiledToCreateAlteration();
        }
    }
}