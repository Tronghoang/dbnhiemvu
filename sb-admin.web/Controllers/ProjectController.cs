using sb_admin.web.Models;
using System;
using System.Collections.Generic;
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
                var list = db.Projects.Where(m=>m.vTenProject.Contains(search)).ToList();
                if (iNgayLap == 1)
                {
                    list = list.OrderBy(m => m.dNgayLap).ToList();
                }
                else {
                    list = list.OrderByDescending(m => m.dNgayLap).ToList();
                }
                var item = 4;
                list = list.Skip(index.Value * item).Take(item).ToList();
                return Json(list);
            }
        }
    }
}