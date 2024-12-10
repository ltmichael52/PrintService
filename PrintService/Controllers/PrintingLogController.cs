using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintService.Models;
using PrintService.Models.Authentication;
using PrintService.ViewModels;

namespace PrintService.Controllers
{
    [AuthenticationM_S]
    public class PrintingLogController : Controller
    {
        private readonly PrintDbContext db;
        public PrintingLogController(PrintDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            string studentId = HttpContext.Session.GetString("AccountID")?? "0";
            List<PrintingLogViewModel> prntLog = db.PrintingLogs.Where(x=>x.StudentId == studentId).Include(x=>x.PaperType)
                                        .Include(x=>x.Document)
                                        .Select(x=> new PrintingLogViewModel
                                        {
                                            Id = x.LogId,
                                            RequestTime = x.StartTime,
                                            FinishedTime = x.EndTime,
                                            IsColored = x.IsColored,
                                            IsDoubled = x.IsDoubleSided,
                                            PaperTypeName = x.PaperType.PaperName,
                                            DocumentName = x.Document.FileName,
                                            NumberOfCopies = x.Copies,
                                            Status = x.Status
                                        }).OrderByDescending(x => x.RequestTime).ToList();
            return View(prntLog);
        }
    }
}
