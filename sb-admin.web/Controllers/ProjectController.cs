using sb_admin.web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sb_admin.web.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChiTietProject()
        {
            return View();
        }
        public ActionResult GetInfoProject(int iMaProjectCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var result = db.Projects.Find(iMaProjectCode);
                return Json(result);
            }
                
        }
        public ActionResult GetProject(int? index, string search, int? iNgayLap)
        {
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
                var list = db.Projects.Where(m => m.vTenProject.Contains(search) && m.iTrangThai==1).ToList();
                if (iNgayLap == 1)
                {
                    list = list.OrderBy(m => m.dNgayLap).ToList();
                }
                else {
                    list = list.OrderByDescending(m => m.dNgayLap).ToList();
                }
                var item = 4;
                list = list.Skip(index.Value * item).Take(item).ToList();
                var result = (from i in list
                              select new ProjectViewModel
                              {
                                  iMaProjectCode=i.iMaProjectCode,
                                  vMoTa = i.vMoTa,
                                  vTenProject = i.vTenProject,
                                  dNgayLap = i.dNgayLap.Value.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US")),
                              }).ToList();
                return Json(result);
            }
        }
        public int ThemProject(ThemProjectViewModel model)
        {
            try {
                var p = new Project();
                p.dNgayLap = DateTime.Now;
                p.iTrangThai = 1;
                p.vMoTa = model.vMoTa;
                p.vTenProject = model.vTenProject;
                using (var db = new dbnhiemvuEntities())
                {
                    db.Projects.Add(p);
                    db.SaveChanges();
                    return p.iMaProjectCode;
                }
            } catch {
                return 0;
            }
            
        }
        public bool ChinhSuaProject(ChinhSuaProjectViewModel model)
        {
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    var p =db.Projects.Find(model.iMaProjectCode);
                    p.vTenProject = model.vTenProject;
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
        public bool XoaProject(int iMaProjectCode)
        {
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    var p = db.Projects.Find(iMaProjectCode);
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