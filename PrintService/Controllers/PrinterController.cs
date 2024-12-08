using Microsoft.AspNetCore.Mvc;
using PrintService.Models;

namespace PrintService.Controllers
{
    public class PrinterController : Controller
    {
        private readonly PrintDbContext db;
        public PrinterController(PrintDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            List<Printer> printerList = db.Printers.ToList();

            return View();
        }
    }
}
