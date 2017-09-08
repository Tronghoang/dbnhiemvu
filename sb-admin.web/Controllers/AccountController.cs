using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sb_admin.web.Models;
using sb_admin.web.Helper;
using System.IO;
using sb_admin.web.Filters;
using System.Globalization;

namespace sb_admin.web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        // GET: Account
        [CheckLogin]
        public ActionResult Index()
        {
            using (var db = new dbnhiemvuEntities())
            {
                ViewBag.ThanhVien = (from t in db.ThanhViens
                                     select new GetThanhVienViewModel
                                     {
                                         iMaThanhVienCode = t.iMaThanhVienCode,
                                         vTenDangNhap = t.vTenDangNhap
                                     }).ToList();
                ViewBag.TrangThai = db.TrangThais.ToList();
                return View();
            }
                
            
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)

        {
            string encPWD = StringUtils.MD5(model.vMatKhau);

            using (var db = new dbnhiemvuEntities())
            {
                var user = db.ThanhViens.Where(m => m.vTenDangNhap == model.vTenDangNhap && m.vMatKhau == encPWD).FirstOrDefault();
                if (user != null)
                {
                    Session["isLogin"] = 1;
                    Session["user"] = user;
                    Session["TaiKhoan"] = user.vTenDangNhap;
                    if (Request.Form.GetValues("bGhiNho") != null)
                    {
                        Response.Cookies["userID"].Value = user.iMaThanhVienCode.ToString();
                        Response.Cookies["userID"].Expires = DateTime.Now.AddDays(7);
                    }
                    return RedirectToAction("ThanhVien", "Home");
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Msg = "Tên đăng nhập hoặc tài khoản không đúng !";
                    return View();
                }
            }
        }
        [HttpPost]
        public bool Register(RegisterViewModel model)
        {
            try
            {
                string encPWD = StringUtils.MD5(model.vMatKhau);
                using (var db = new dbnhiemvuEntities())
                {
                    var user = new ThanhVien()
                    {
                        vTenDangNhap = model.vTenDangNhap,
                        vMatKhau = encPWD,
                        iTrangThai = 1
                    };

                    db.ThanhViens.Add(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult Logout()
        {
            CurrentContext.Destroy();
            return RedirectToAction("ThanhVien", "Home");
        }
    }
}