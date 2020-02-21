using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GDStore.DAL.Interface.Services;

namespace GDStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<ActionResult> Index()
        {
            return View((await customerRepository.GetAllAsync()).ToList());
        }
    }
}