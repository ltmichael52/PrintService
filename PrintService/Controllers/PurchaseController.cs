using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintService.Models;
using PrintService.Models.Authentication;
using PrintService.ViewModels;

namespace PrintService.Controllers
{
    [AuthenticationM_S]
    public class PurchaseController : Controller
    {
        private readonly PrintDbContext _db = new PrintDbContext();

        public IActionResult Index()
        {
            // Lấy studentId từ session
            string studentId = HttpContext.Session.GetString("AccountID");
            if (string.IsNullOrEmpty(studentId))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng về trang đăng nhập nếu session không tồn tại
            }

            var student = _db.Students.SingleOrDefault(s => s.StudentId == studentId);

            if (student == null)
            {
                return HttpNotFound("Student not found");
            }

            var paperDetails = _db.PaperDetailStudents
                .Where(p => p.StudentId == studentId && (p.PaperTypeId == 1 || p.PaperTypeId == 2))
                .ToList();

            var paperPrices = _db.PaperTypes
                .Where(p => p.PaperTypeId == 1 || p.PaperTypeId == 2)
                .ToList();

            var model = new BuyPaperViewModel
            {
                A3Balance = paperDetails.FirstOrDefault(p => p.PaperTypeId == 1)?.Amount ?? 0,
                A4Balance = paperDetails.FirstOrDefault(p => p.PaperTypeId == 2)?.Amount ?? 0,
                AccountBalance = (decimal)student.AccountBalance,
                A3Price = paperPrices.FirstOrDefault(p => p.PaperTypeId == 1)?.Price ?? 0,
                A4Price = paperPrices.FirstOrDefault(p => p.PaperTypeId == 2)?.Price ?? 0,
            };

            if (TempData["SuccessPurchase"] != null)
            {
                ViewBag.SuccessPurchase = TempData["SuccessPurchase"];
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BuyPaperViewModel model)
        {
            // Lấy studentId từ session
            string studentId = HttpContext.Session.GetString("AccountID");
            if (string.IsNullOrEmpty(studentId))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng về trang đăng nhập nếu session không tồn tại
            }

            var student = _db.Students.SingleOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var paperDetails = _db.PaperDetailStudents
                .Where(p => p.StudentId == studentId && (p.PaperTypeId == 1 || p.PaperTypeId == 2))
                .ToList();

            var paperPrices = _db.PaperTypes
                .Where(p => p.PaperTypeId == 1 || p.PaperTypeId == 2)
                .ToList();

            if (model.TotalAmount > student.AccountBalance)
            {
                ViewBag.InvalidPaper = true;

                // Re-populate model with existing values
                model.A3Balance = paperDetails.FirstOrDefault(p => p.PaperTypeId == 1)?.Amount ?? 0;
                model.A4Balance = paperDetails.FirstOrDefault(p => p.PaperTypeId == 2)?.Amount ?? 0;
                model.AccountBalance = (decimal)student.AccountBalance;
                model.A3Price = paperPrices.FirstOrDefault(p => p.PaperTypeId == 1)?.Price ?? 0;
                model.A4Price = paperPrices.FirstOrDefault(p => p.PaperTypeId == 2)?.Price ?? 0;

                return View(model);
            }

            // Thực hiện giao dịch
            var purchaseHistory = new PurchaseHistory
            {
                StudentId = student.StudentId,
                PurchasedDate = DateTime.Now,
                TotalPurchased = model.TotalAmount
            };

            _db.PurchaseHistories.Add(purchaseHistory);
            _db.SaveChanges();

            // Xử lý chi tiết giao dịch
            if (model.A3Quantity > 0)
            {
                _db.PurchaseHistoryDetails.Add(new PurchaseHistoryDetail
                {
                    PurchaseId = purchaseHistory.PurchaseId,
                    PaperTypeId = 1,
                    PaperPurchased = model.A3Quantity,
                    PurchasedByType = model.A3Quantity * model.A3Price
                });
            }

            if (model.A4Quantity > 0)
            {
                _db.PurchaseHistoryDetails.Add(new PurchaseHistoryDetail
                {
                    PurchaseId = purchaseHistory.PurchaseId,
                    PaperTypeId = 2,
                    PaperPurchased = model.A4Quantity,
                    PurchasedByType = model.A4Quantity * model.A4Price
                });
            }

            // Cập nhật PaperDetailStudent
            var paperDetailA3 = _db.PaperDetailStudents.SingleOrDefault(p => p.StudentId == studentId && p.PaperTypeId == 1);
            if (paperDetailA3 != null)
            {
                paperDetailA3.Amount += model.A3Quantity;
            }
            else
            {
                _db.PaperDetailStudents.Add(new PaperDetailStudent
                {
                    StudentId = studentId,
                    PaperTypeId = 1,
                    Amount = model.A3Quantity
                });
            }

            var paperDetailA4 = _db.PaperDetailStudents.SingleOrDefault(p => p.StudentId == studentId && p.PaperTypeId == 2);
            if (paperDetailA4 != null)
            {
                paperDetailA4.Amount += model.A4Quantity;
            }
            else
            {
                _db.PaperDetailStudents.Add(new PaperDetailStudent
                {
                    StudentId = studentId,
                    PaperTypeId = 2,
                    Amount = model.A4Quantity
                });
            }

            // Trừ số dư tài khoản
            student.AccountBalance -= model.TotalAmount;
            _db.SaveChanges();
            TempData["SuccessPurchase"] = true;

            return RedirectToAction("Index");
        }

        private IActionResult HttpNotFound(string message)
        {
            return NotFound(new { error = message });
        }
    }
}
