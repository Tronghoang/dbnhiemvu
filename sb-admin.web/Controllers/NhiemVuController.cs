using sb_admin.web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sb_admin.web.Helper;
using sb_admin.web.Filters;
namespace sb_admin.web.Controllers
{
    [CheckLogin]
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
                              join tt in db.TrangThais on nv.iMaTrangThaiCode equals tt.iMaTrangThaiCode
                              where nv.iMaModuleCode == iMaModuleCode
                              select new LayNhiemVu
                              {
                                  iMaNhiemVuCode = nv.iMaNhiemVuCode,
                                  vNguoiDang = tv.vTenDangNhap,
                                  vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                                  vMoTa = nv.vMoTa,
                                  vTenNhiemVu = nv.vTenNhiemVu,
                                  iMaTrangThaiCode = nv.iMaTrangThaiCode,
                                  dNgayLap = nv.dNgayLap,
                                  dNgayBD = nv.dNgayBD,
                                  dNgayKT = nv.dNgayKT,
                                  vTenTrangThai = tt.vTenTrangThai
                              }).OrderByDescending(m => m.dNgayLap).ToList();
                return Json(result);
            }
        }
        public ActionResult HinhNhiemVuPartial(int iMaNhiemVuCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var result = db.Hinhs.Where(m => m.iMaNhiemVuCode == iMaNhiemVuCode).ToList();
                return PartialView("_HinhNhiemVuPartial", result);
                //return Json(result);
            }

        }
        public ActionResult GetThongTinThanhVien()
        {
            var iMaThanhVienCode = CurrentContext.GetUser().iMaThanhVienCode;
            using (var db = new dbnhiemvuEntities())
            {
                var result = (from t in db.ThanhViens
                              join n in db.NhiemVus on t.iMaThanhVienCode equals n.iMaNguoiDuocGiaoCode
                              where t.iMaThanhVienCode != 14 && t.iMaThanhVienCode != iMaThanhVienCode
                              group new { n.iMaTrangThaiCode, n.iMaNhiemVuCode } by new { t.iMaThanhVienCode, t.vTenDangNhap } into g
                              select new NhiemVuThanhVienViewModel
                              {
                                  vTenDangNhap = g.Key.vTenDangNhap,
                                  iMaThanhVienCode = g.Key.iMaThanhVienCode,
                                  iSoNhiemVuMoi = g.Where(m => m.iMaTrangThaiCode == 1).Count(),
                                  iSoNhiemVuDangThucHien = g.Where(m => m.iMaTrangThaiCode == 2).Count(),
                                  iSoNhiemVuDangChoDuyet = g.Where(m => m.iMaTrangThaiCode == 3).Count(),
                                  iSoNhiemVuDaHoanThanh = g.Where(m => m.iMaTrangThaiCode == 4).Count(),
                                  iSoNhiemVuDangSuaLoi = g.Where(m => m.iMaTrangThaiCode == 5).Count(),
                                  iSoNhiemVuDuocGiao = g.Count()
                              }).ToList();
                return Json(result);
            }
        }
        public ActionResult ChiTietNhiemVu(int? iMaNhiemVuCode)
        {
            if (!iMaNhiemVuCode.HasValue)
            {
                return RedirectToAction("Index", "Project");
            }
            using (var db = new dbnhiemvuEntities())
            {
                var result = (from t in db.NhiemVus
                              join s in db.ThanhViens on t.iMaNguoiDangCode equals s.iMaThanhVienCode
                              join r in db.ThanhViens on t.iMaNguoiDuocGiaoCode equals r.iMaThanhVienCode
                              join tt in db.TrangThais on t.iMaTrangThaiCode equals tt.iMaTrangThaiCode
                              where t.iMaNhiemVuCode == iMaNhiemVuCode
                              select new ChiTietNhiemVuViewModel
                              {
                                  iMaNguoiDangCode = t.iMaNguoiDangCode,
                                  iMaNguoiDuocGiaoCode = t.iMaNguoiDuocGiaoCode,
                                  iMaNhiemVuCode = t.iMaNhiemVuCode,
                                  iMaTrangThaiCode = t.iMaTrangThaiCode,
                                  dNgayBD = t.dNgayBD,
                                  dNgayKT = t.dNgayKT,
                                  dNgayLap = t.dNgayLap,
                                  vMoTa = t.vMoTa,
                                  vTenNguoiDang = s.vTenDangNhap,
                                  vTenNguoiDuocGiao = r.vTenDangNhap,
                                  vTenNhiemVu = t.vTenNhiemVu,
                                  vTenTrangThai = tt.vTenTrangThai
                              }).FirstOrDefault();
                return View(result);
            }


        }
        public ActionResult GetNhiemVuTrangThai(int? iMaNguoiDuocGiaoCode, int? iMaTrangThaiCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var list = db.NhiemVus.ToList();
                if (iMaNguoiDuocGiaoCode.HasValue && iMaNguoiDuocGiaoCode != 0)
                {
                    list = list.Where(m => m.iMaNguoiDuocGiaoCode == iMaNguoiDuocGiaoCode).ToList();
                }
                if (iMaTrangThaiCode.HasValue && iMaTrangThaiCode != 0)
                {
                    list = list.Where(m => m.iMaTrangThaiCode == iMaTrangThaiCode).ToList();
                }
                var result = (from nv in list
                              join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                              join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
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
        public int ThemNhiemVu(ThemNhiemVuMoel model)
        {
            var iMaNguoiDangCode = CurrentContext.GetUser().iMaThanhVienCode;
            var task = new Models.NhiemVu();
            task.dNgayLap = DateTime.Now;
            task.dNgayBD = model.dNgayBD;
            task.dNgayKT = model.dNgayKT;
            task.iMaNguoiDuocGiaoCode = 14;
            task.vTenNhiemVu = model.vTenNhiemVu;
            task.vMoTa = model.vMoTa;
            task.iMaNguoiDangCode = iMaNguoiDangCode;
            task.iMaTrangThaiCode = 1;
            task.iMaModuleCode = model.iMaModuleCode;
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    db.NhiemVus.Add(task);
                    db.SaveChanges();
                }
                return task.iMaNhiemVuCode;
            }
            catch
            {
                return 0;
            }

        }
        public bool GiaoNhiemVu(int iMaNhiemVuCode, int iMaThanhVienCode)
        {
            try {
                using (var db = new dbnhiemvuEntities())
                {
                    var n = db.NhiemVus.Find(iMaNhiemVuCode);
                    n.iMaNguoiDuocGiaoCode = iMaThanhVienCode;
                    n.iMaTrangThaiCode = 1;
                    db.SaveChanges();
                    return true;
                }
            } catch {
                return false;
            }
        }
        public bool XoaNhiemVu(int id)
        {
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    var n = db.NhiemVus.Find(id);
                    n.iMaTrangThaiCode = 0;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool UploadFile(int iMaNhiemVuCode)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        string[] extHinh = new string[] { ".jpg", ".png" };
                        string[] extFile = new string[] { ".xls", ".xlsx", ".doc", ".docx", ".txt", ".zip", ".rar", ".sql", ".cs", ".js", ".css" };
                        using (var db = new dbnhiemvuEntities())
                        {
                          
                            // kiểm tra định dạng file là .jpg hoặc .png
                            if (extHinh.Contains(Path.GetExtension(file.FileName).ToLower()))
                            {
                                // tạo thư mục lưu hình
                                string spDirPath = Server.MapPath("~/Content/Images");
                                string targetDirpath = Path.Combine(spDirPath, iMaNhiemVuCode.ToString());
                                Directory.CreateDirectory(targetDirpath);
                                // lấy tên file
                                var fileName = Path.GetFileName(file.FileName);
                                // tạo đường dẫn và lưu hình ảnh
                                var path = Path.Combine(Server.MapPath($"~/Content/Images/{iMaNhiemVuCode}/"), fileName);
                                file.SaveAs(path);
                                // lưu hình ảnh vào cơ sở dữ liệu
                                var image = new Hinh();
                                image.vDuongDan = $"/Content/Images/{iMaNhiemVuCode}/{fileName}";
                                image.iMaNhiemVuCode = iMaNhiemVuCode;
                                image.iTrangThai = 1;
                                db.Hinhs.Add(image);
                                db.SaveChanges();
                            }
                            else if (extFile.Contains(Path.GetExtension(file.FileName).ToLower()))
                            {

                                // tạo thư mục lưu file
                                var spDirPath = Server.MapPath("~/Content/Files");
                                var targetDirpath = Path.Combine(spDirPath, iMaNhiemVuCode.ToString());
                                Directory.CreateDirectory(targetDirpath);
                                // lấy tên file
                                var fileName = Path.GetFileName(file.FileName);
                                // tạo đường dẫn và lưu hình ảnh
                                var path = Path.Combine(Server.MapPath($"~/Content/Files/{iMaNhiemVuCode}/"), fileName);
                                file.SaveAs(path);
                                // lưu hình ảnh vào cơ sở dữ liệu
                                var taptin = new TapTin();
                                //kiểm tra xem tập tin này đã tồn tại hay chưa
                                var checktaptin = db.TapTins.Where(x => x.vTenTapTin == fileName).Count();
                                if(checktaptin != 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    taptin.vDuongDan = $"/Content/Files/{iMaNhiemVuCode}/{fileName}";
                                    taptin.iMaNhiemVuCode = iMaNhiemVuCode;
                                    taptin.vTenTapTin = fileName;
                                    taptin.iTrangThai = 1;
                                    db.TapTins.Add(taptin);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ActionResult GetTapTinNhiemVu(int iMaNhiemVuCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var result = db.TapTins.Where(m => m.iMaNhiemVuCode == iMaNhiemVuCode).ToList();
                return Json(result);
            }

        }
        public bool ThayDoiTrangThai(int iMaNhiemVuCode, int iMaTrangThaiCode)
        {
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    var task = db.NhiemVus.Find(iMaNhiemVuCode);
                    task.iMaTrangThaiCode = iMaTrangThaiCode;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }
        public bool UploadFileBaoCao(int iMaBaoCaoCode)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        string[] extFile = new string[] { ".xls", ".xlsx", ".doc", ".docx", ".txt", ".zip", ".rar", ".sql", ".cs", ".js", ".css" };
                        using (var db = new dbnhiemvuEntities())
                        {
                            if (extFile.Contains(Path.GetExtension(file.FileName).ToLower()))
                            {
                                // tạo thư mục lưu file
                                var spDirPath = Server.MapPath("~/Content/FileReports");
                                var targetDirpath = Path.Combine(spDirPath, iMaBaoCaoCode.ToString());
                                Directory.CreateDirectory(targetDirpath);
                                // lấy tên file
                                var fileName = Path.GetFileName(file.FileName);
                                // tạo đường dẫn và lưu hình ảnh
                                var path = Path.Combine(Server.MapPath($"~/Content/FileReports/{iMaBaoCaoCode}/"), fileName);
                                file.SaveAs(path);
                                // lưu hình ảnh vào cơ sở dữ liệu
                                var taptin = new TapTinBaoCao();
                                taptin.vDuongDan = $"/Content/FileReports/{iMaBaoCaoCode}/{fileName}";
                                taptin.iMaBaoCaoCode = iMaBaoCaoCode;
                                taptin.vTenTapTinBaoCao = fileName;
                                taptin.iTrangThai = 1;
                                db.TapTinBaoCaos.Add(taptin);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return true;
            }
            catch { return false; }
        }
        public int BaoCaoNhiemVu(BaoCaoNhiemVuViewModel model)
        {
            try
            {
                using (var db = new dbnhiemvuEntities())
                {
                    var baocao = new BaoCao();
                    baocao.iMaNhiemVuCode = model.iMaNhiemVuCode;
                    baocao.vMoTa = model.vMoTa;
                    baocao.vDuongDanTapTin = model.vDuongDanTapTin;
                    db.BaoCaos.Add(baocao);
                    db.SaveChanges();
                    return baocao.iMaBaoCaoCode;
                }
            }
            catch
            {
                return 0;
            }

        }
        public int ThemLoi(ThemLoiViewModel model)
        {
            var loi = new ChiTietLoi();
            loi.vChiTietLoi = model.vChiTietLoi;
            loi.vTenLoi = model.vTenLoi;
            loi.iMaNhiemVuCode = model.iMaNhiemVuCode;
            loi.iTrangThai = 1;
            try {
                using (var db = new dbnhiemvuEntities())
                {
                    var nv = db.NhiemVus.Find(model.iMaNhiemVuCode);
                    nv.iMaTrangThaiCode = 5;
                    db.ChiTietLois.Add(loi);
                    db.SaveChanges();
                    return loi.iMaChiTietLoiCode;
                }
            } catch {
                return 0;
            }

        }
        public bool UploadHinhLoi(int iMaChiTietLoiCode)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        string[] extHinh = new string[] { ".jpg", ".png" };
                        using (var db = new dbnhiemvuEntities())
                        {
                            // kiểm tra định dạng file là .jpg hoặc .png
                            if (extHinh.Contains(Path.GetExtension(file.FileName).ToLower()))
                            {
                                // tạo thư mục lưu hình
                                string spDirPath = Server.MapPath("~/Content/ImageErr");
                                string targetDirpath = Path.Combine(spDirPath, iMaChiTietLoiCode.ToString());
                                Directory.CreateDirectory(targetDirpath);
                                // lấy tên file
                                var fileName = Path.GetFileName(file.FileName);
                                // tạo đường dẫn và lưu hình ảnh
                                var path = Path.Combine(Server.MapPath($"~/Content/ImageErr/{iMaChiTietLoiCode}/"), fileName);
                                file.SaveAs(path);
                                // lưu hình ảnh vào cơ sở dữ liệu
                                var image = new HinhLoi();
                                image.vDuongDan = $"/Content/ImageErr/{iMaChiTietLoiCode}/{fileName}";
                                image.iMaChiTietLoiCode = iMaChiTietLoiCode;
                                image.iTrangThai = 1;
                                db.HinhLois.Add(image);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ActionResult GetChiTietLoi(int iMaNhiemVuCode)
        {
            using (var db = new dbnhiemvuEntities()) {
                var result = db.ChiTietLois.Where(m => m.iMaNhiemVuCode == iMaNhiemVuCode).ToList();
                return PartialView("_ChiTietLoiPartial", result);

            }

        }
        public ActionResult HinhLoiPartial(int iMaChiTietLoiCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var result = db.HinhLois.Where(m => m.iMaChiTietLoiCode == iMaChiTietLoiCode).ToList();
                return PartialView("_HinhLoiPartial", result);
                //return Json(result);
            }

        }
        public ActionResult GetBaoCao(int iMaNhiemVuCode)
        {
            using (var db = new dbnhiemvuEntities())
            {
                var result = db.ChiTietLois.Where(m => m.iMaNhiemVuCode == iMaNhiemVuCode).ToList();
                return PartialView("_BaoCaoPartial", result);

            }
        }
    }
}