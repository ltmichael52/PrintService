using Microsoft.AspNetCore.Mvc;

namespace PrintService.Controllers
{
    public class RechargeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
