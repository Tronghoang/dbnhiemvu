using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sb_admin.web.Models;
using System.Globalization;

namespace sb_admin.web.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult ByProject(int? iMaProjectCode)
        {
            if (!iMaProjectCode.HasValue)
            {
                return RedirectToAction("Index", "Project");
            }
            ViewBag.iMaProjectCode = iMaProjectCode;
            using (var db = new dbnhiemvuEntities())
            {
                var p = db.Projects.Find(iMaProjectCode);
                ViewBag.vTenProject = p.vTenProject;
                return View();
            }
                
        }
        public ActionResult GetInfoModule(int iMaModuleCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var result = db.Modules.Find(iMaModuleCode);
                return Json(result);
            }

        }
        public ActionResult GetModule(int? index, string search, int? iNgayLap,int? iMaProjectCode)
        {
            if (!iMaProjectCode.HasValue)
            {
                return RedirectToAction("ThanhVien", "Home");
            }
            if (!index.HasValue)
            {
                index = 0;
            }
            if (search == null)
            {
                search = "";
            }
            if (!iNgayLap.HasValue)
            {
                iNgayLap = 1;
            }
            using (var db = new dbnhiemvuEntities())
            {
                var list = db.Modules.Where(m => m.vTenModule.Contains(search) && m.iTrangThai == 1 && m.iMaProjectCode== iMaProjectCode).ToList();
                if (iNgayLap == 1)
                {
                    list = list.OrderBy(m => m.dNgayLap).ToList();
                }
                else {
                    list = list.OrderByDescending(m => m.dNgayLap).ToList();
                }
                var item = 4;
                list = list.Skip(index.Value * item).Take(item).ToList();
                var result = (from m in list
                              select new ModuleViewModel
                              {
                                  vTenModule = m.vTenModule,
                                  vMoTa=m.vMoTa,
                                  iMaModuleCode = m.iMaModuleCode,
                                  dNgayLap = m.dNgayLap.Value.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US")),
                              }).ToList();
                return Json(result);
            }
        }
        public int ThemModule(ThemModuleViewModel model)
        {
            try
            {
                var m = new Module();
                m.dNgayLap = DateTime.Now;
                m.iTrangThai = 1;
                m.vMoTa = model.vMoTa;
                m.vTenModule = model.vTenModule;
                m.iMaProjectCode = model.iMaProjectCode;
                using (var db = new dbnhiemvuEntities())
                {
                    db.Modules.Add(m);
                    db.SaveChanges();
                    return m.iMaModuleCode;
                }
            }
            catch
            {
                return 0;
            }

        }
        public bool ChinhSuaModule(ChinhSuaModuleViewModel model)
        {
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    var p = db.Modules.Find(model.iMaModuleCode);
                    p.vTenModule = model.vTenModule;
                    p.vMoTa = model.vMoTa;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }
        public bool XoaModule(int iMaModuleCode)
        {
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    var p = db.Modules.Find(iMaModuleCode);
                    p.iTrangThai = 0;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}