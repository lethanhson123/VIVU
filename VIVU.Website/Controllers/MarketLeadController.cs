using Microsoft.AspNetCore.Mvc;

namespace VIVU.Website.Controllers
{
    public class MarketLeadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
