using System.Linq;
using System.Web.Mvc;
using GDStore.DAL.Interface.Services;

namespace GDStore.WebApi.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            return View(customerRepository.GetAll().ToList());
        }
    }
}