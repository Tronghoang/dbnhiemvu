using System;
using System.Collections.Generic;
using System.Linq;
using sb_admin.web.Models;
using System.Web;

namespace sb_admin.web.Helper
{
    public class CurrentContext
    {
        public static bool IsLogged()
        {
            var flag = HttpContext.Current.Session["isLogin"];
            if (flag == null || (int)flag == 0)
            {
                // Kiểm tra trong cookie
                // nếu có cookie , dùng thông tin trong cookie
                // để tái tạo lại session
                if (HttpContext.Current.Request.Cookies["userID"] != null)
                {
                    int userIdCookie = Convert.ToInt32(HttpContext.Current.Request.Cookies["userID"].Value);
                    using (var db = new dbnhiemvuEntities())
                    {
                        var user = db.ThanhViens.Where(u => u.iMaThanhVienCode == userIdCookie).FirstOrDefault();
                        HttpContext.Current.Session["isLogin"] = 1;
                        HttpContext.Current.Session["user"] = user;
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return true;
            }
        }

        public static ThanhVien GetUser()
        {
            return (ThanhVien)HttpContext.Current.Session["user"];
        }
        public static void Destroy()
        {
            HttpContext.Current.Session["isLogin"] = 0;
            HttpContext.Current.Session["user"] = null;
            if (HttpContext.Current.Request.Cookies["userID"] != null)
            {
                HttpContext.Current.Response.Cookies["userID"].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}