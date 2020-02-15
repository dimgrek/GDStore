using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GDStore.WebApi.Models;
using GDStore.WebApi.Services;

//using GDStore.Models;
//using GDStore.Services;

namespace GDStore.WebApi.Controllers
{
    public class AlterationController : Controller
    {
        private readonly IAlterationService alterationService;


        public AlterationController(IAlterationService alterationService)
        {
            this.alterationService = alterationService;
        }

        public ActionResult Index()
        {
            return View(alterationService.GetAll());
        }

        public ActionResult AlterationsByCustomerId(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            //todo: make GetAll method async in Repo
            return View(alterationService.GetAllByCustomerId(customerId));
        }

        public ActionResult Add(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerId,Name,Item,Side,Length")]AlterationModel model)
        {
            //if (ModelState.IsValid)
            //{
                await alterationService.AddAlteration(model);

                return RedirectToAction(nameof(AlterationsByCustomerId), new { model.CustomerId });
            //}

            //return BadRequest();
        }
    }
}