using sb_admin.web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sb_admin.web.Controllers
{
    public class NhiemVuController : Controller
    {
        // GET: NhiemVu
        public ActionResult ByModule(int? iMaModuleCode)
        {
            if (!iMaModuleCode.HasValue)
            {
                return RedirectToAction("Index", "Project");
            }
            ViewBag.iMaModuleCode = iMaModuleCode;
            using (var db = new dbnhiemvuEntities())
            {
                var p = db.Modules.Find(iMaModuleCode);
                ViewBag.vTenModule = p.vTenModule;
                ViewBag.vTenProject = db.Projects.Find(p.iMaProjectCode).vTenProject;
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
        public ActionResult GetNhiemVuModule(int iMaModuleCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var result = (from nv in db.NhiemVus
                               join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                               join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                               where nv.iMaModuleCode ==iMaModuleCode
                               select new LayNhiemVu
                               {
                                   iMaNhiemVuCode = nv.iMaNhiemVuCode,
                                   vNguoiDang = tv.vTenDangNhap,
                                   vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                                   vMoTa = nv.vMoTa,
                                   vTenNhiemVu = nv.vTenNhiemVu,
                                   iMaTrangThaiCode = nv.iMaTrangThaiCode,
                                   dNgayBD = nv.dNgayBD,
                                   dNgayKT = nv.dNgayKT,
                               }).ToList();
                return Json(result);
            }
        }

    }
}