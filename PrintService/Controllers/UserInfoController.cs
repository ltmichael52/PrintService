using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintService.Models;
using PrintService.Models.Authentication;
using PrintService.ViewModels;

namespace PrintService.Controllers
{
    [AuthenticationM_S]
    public class UserInfoController : Controller
    {
        private readonly PrintDbContext db;
        public UserInfoController(PrintDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ShowStudent stu = db.Students.Include(x => x.PaperDetailStudents).Where(x => x.StudentId == HttpContext.Session.GetString("AccountID"))
                 .Select(x=>new ShowStudent
                 {
                     studentId = x.StudentId,
                     Name = x.FullName,
                     Email = x.Email,
                     A3Amount = x.PaperDetailStudents.FirstOrDefault(x=>x.PaperTypeId == 1).Amount ?? 0,
                     A4Amount = x.PaperDetailStudents.FirstOrDefault(x => x.PaperTypeId == 2).Amount ?? 0,
                     Balance = x.AccountBalance ?? 0
                 }).FirstOrDefault() ??new ShowStudent();
            return View(stu);
        }
    }
}
