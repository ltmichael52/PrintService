using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintService.Models;
using PrintService.ViewModels;

namespace PrintService.Controllers
{
    
    public class PurchaseController : Controller
    {
        private readonly PrintDbContext _db = new PrintDbContext();

        public IActionResult Index()
        {
            // Ví dụ: Lấy dữ liệu sinh viên hiện tại
            string studentId = "S001"; // Thay bằng ID sinh viên thực tế (đăng nhập)
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


            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BuyPaperViewModel model)
        {
            // Lấy thông tin từ ViewModel (số lượng giấy và tổng tiền)
            int quantityA3 = model.A3Quantity;
            int quantityA4 = model.A4Quantity;
            decimal totalAmount = model.TotalAmount;

            // Xử lý giao dịch mua giấy (cập nhật tài khoản sinh viên, thêm vào lịch sử giao dịch, vv.)
            var student = _db.Students.SingleOrDefault(s => s.StudentId == "S001"); // Lấy thông tin sinh viên
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Kiểm tra số dư tài khoản
            if (student.AccountBalance < totalAmount)
            {
                return BadRequest("Insufficient account balance");
            }

            // Cập nhật giao dịch mua
            var purchaseHistory = new PurchaseHistory
            {
                StudentId = student.StudentId,
                PurchasedDate = DateTime.Now,
                TotalPurchased = totalAmount
            };

            _db.PurchaseHistories.Add(purchaseHistory);
            _db.SaveChanges();

            var pricesA3 = _db.PaperTypes
                .Where(p =>p.PaperTypeId == 1)
                .Select(p => p.Price)
                .First();

            var pricesA4 = _db.PaperTypes
                .Where(p => p.PaperTypeId == 2)
                .Select(p => p.Price)
                .First();
            // Cập nhật thông tin chi tiết mua
            if (quantityA3 > 0)
            {
                _db.PurchaseHistoryDetails.Add(new PurchaseHistoryDetail
                {
                    PurchaseId = purchaseHistory.PurchaseId,
                    PaperTypeId = 1, // A3
                    PaperPurchased = quantityA3,
                    PurchasedByType = (decimal)(quantityA3 * pricesA3)
                });
            }

            if (quantityA4 > 0)
            {
                _db.PurchaseHistoryDetails.Add(new PurchaseHistoryDetail
                {
                    PurchaseId = purchaseHistory.PurchaseId,
                    PaperTypeId = 2, // A4
                    PaperPurchased = quantityA4,
                    PurchasedByType = (decimal)(quantityA4 * pricesA4)
                });
            }

            // Cập nhật thông tin chi tiết mua giấy cho PaperDetailStudent
            // Cập nhật giấy A3
            var paperDetailA3 = _db.PaperDetailStudents
                                    .SingleOrDefault(p => p.StudentId == student.StudentId && p.PaperTypeId == 1); // A3

            if (paperDetailA3 != null)
            {
                paperDetailA3.Amount += quantityA3; // Cộng thêm số lượng A3
            }
            else
            {
                // Nếu không có giấy A3, tạo mới
                _db.PaperDetailStudents.Add(new PaperDetailStudent
                {
                    StudentId = student.StudentId,
                    PaperTypeId = 1, // A3
                    Amount = quantityA3
                });
            }

            // Cập nhật giấy A4
            var paperDetailA4 = _db.PaperDetailStudents
                                    .SingleOrDefault(p => p.StudentId == student.StudentId && p.PaperTypeId == 2); // A4

            if (paperDetailA4 != null)
            {
                paperDetailA4.Amount += quantityA4; // Cộng thêm số lượng A4
            }
            else
            {
                // Nếu không có giấy A4, tạo mới
                _db.PaperDetailStudents.Add(new PaperDetailStudent
                {
                    StudentId = student.StudentId,
                    PaperTypeId = 2, // A4
                    Amount = quantityA4
                });
            }


            _db.SaveChanges();
            // Trừ tiền từ tài khoản sinh viên
            student.AccountBalance -= totalAmount;
            _db.SaveChanges();

            return RedirectToAction("Index"); // Redirect lại trang sau khi hoàn tất giao dịch
        }


        private IActionResult HttpNotFound(string message)
        {
            return NotFound(new { error = message });
        }

    }
}
