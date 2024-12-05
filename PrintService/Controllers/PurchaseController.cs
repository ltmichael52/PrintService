using Microsoft.AspNetCore.Mvc;

namespace PrintService.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
