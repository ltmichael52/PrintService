using Microsoft.AspNetCore.Mvc;

namespace PrintService.Controllers
{
    public class ChoosePrinterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
