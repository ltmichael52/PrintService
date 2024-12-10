using Microsoft.AspNetCore.Mvc;
using PrintService.Models;

namespace PrintService.Controllers
{
    public class AccountController : Controller
    {
        private readonly PrintDbContext _db;
        public AccountController(PrintDbContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (HttpContext.Session.GetString("AccountID") == null)
            {
                // Nếu chưa đăng nhập, hiển thị trang đăng nhập
                return View();
            }
            else
            {
                // Nếu đã đăng nhập, chuyển hướng đến trang Home
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (HttpContext.Session.GetString("AccountID") == null || HttpContext.Session.GetInt32("TypeAccount") != 0)
            {
                // Tìm kiếm tài khoản trong cơ sở dữ liệu
                var user = _db.Accounts.FirstOrDefault(x => x.AccountId == account.AccountId && x.Password == account.Password && x.TypeAccount == 0);

                if (user != null)
                {
                    // Lưu thông tin người dùng vào Session
                    HttpContext.Session.SetString("AccountID", user.AccountId);
                    HttpContext.Session.SetInt32("TypeAccount", user.TypeAccount);

                    
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(account);
        }

        public IActionResult Logout()
        {
            // Xóa thông tin người dùng khỏi Session
            HttpContext.Session.Remove("AccountID");
            HttpContext.Session.Remove("TypeAccount");
            // Chuyển hướng đến trang Login
            return RedirectToAction("Login", "Account");
        }
    }
}
