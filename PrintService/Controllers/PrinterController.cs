using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintService.Models;
using PrintService.ViewModels;

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
            int A3Id = db.PaperTypes.FirstOrDefault(x => x.PaperName == "A3").PaperTypeId;
            int A4Id = db.PaperTypes.FirstOrDefault(x => x.PaperName == "A4").PaperTypeId;

            List<ShowPrinter> printerList = db.Printers.Include(x => x.PaperDetailPrinters)
                                            .Select(x => new ShowPrinter
                                            {
                                                PrinterId = x.PrinterId,
                                                PrinterModel = x.Model,
                                                CampusName = x.CampusName,
                                                BuildingName = x.BuildingName,
                                                RoomName = x.RoomNumber,
                                                IsActive = x.IsActive,
                                                AmountInQueue = db.PrintingLogs.Where(y=>y.PrinterId == x.PrinterId && y.Status==0).Count(),
                                                A3Amount = x.PaperDetailPrinters.FirstOrDefault(a=>a.PaperTypeId == A3Id).Amount,
                                                A4Amount = x.PaperDetailPrinters.FirstOrDefault(a => a.PaperTypeId == A4Id).Amount,
                                            }).ToList();

            return View(printerList);
        }
    }
}
