using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PrintService.Models.Authentication
{
    public class AuthenticationM_S : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loaiTK = context.HttpContext.Session.GetInt32("TypeAccount");
            if (context.HttpContext.Session.GetString("AccountID") == null || (loaiTK != 0 && loaiTK != 1))
            {   // Kiểm tra có tài khoản đăng nhập không và đây có phải loại tài khoản sv hoặc giảng viên
                // Lấy cái session TypeAccount đã lưu trong account controller ra dùng để so sánh
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller", "Account" },
                    {"Action", "Login" },

                });
            }
        }
    }
}
