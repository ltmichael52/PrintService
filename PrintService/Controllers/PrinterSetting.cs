using Microsoft.AspNetCore.Mvc;

namespace PrintService.Controllers
{
    public class PrinterSetting : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
