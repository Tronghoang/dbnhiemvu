using sb_admin.web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sb_admin.web.Models;
using Newtonsoft.Json;
using System.Net;
using PagedList;
using System.Data.Entity.Validation;
using System.IO;
using System.Drawing;
using sb_admin.web.Helper;

namespace sb_admin.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult NhiemVu()
        {

            dbnhiemvuEntities db = new dbnhiemvuEntities();
            ViewBag.ListNguoiDuocGiaoTask = db.ThanhViens.ToList();
            return View();
        }
        
        public ActionResult ThemNhiemVu(string vTenNhiemVu,string vMoTa, int iMaThanhVienCode, DateTime dNgayBD,DateTime dNgayKT)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            Models.NhiemVu nhiemvu = new Models.NhiemVu();
            List<LayNhiemVu> laynhiemvuvualuu = new List<Models.LayNhiemVu>();
            try
            {
               
                nhiemvu.vTenNhiemVu = vTenNhiemVu;
                nhiemvu.vMoTa = vMoTa;
                nhiemvu.iMaNguoiDuocGiaoCode = iMaThanhVienCode;
                nhiemvu.iMaNguoiDangCode = 1;
                nhiemvu.dNgayBD = dNgayBD;
                nhiemvu.dNgayKT = dNgayKT;
                nhiemvu.iMaTrangThaiCode = 1;
                db.NhiemVus.Add(nhiemvu);
                db.SaveChanges();

                laynhiemvuvualuu = (from nv in db.NhiemVus
                                   join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                                   join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                                   where nv.iMaNhiemVuCode==nhiemvu.iMaNhiemVuCode
                                   select new LayNhiemVu
                                   {
                                       iMaNhiemVuCode = nv.iMaNhiemVuCode,
                                       vNguoiDang = tv.vTenDangNhap,
                                       vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                                       vMoTa = nv.vMoTa,
                                       vTenNhiemVu = nv.vTenNhiemVu,
                                       dNgayBD = nv.dNgayBD,
                                       dNgayKT = nv.dNgayKT,
                                       iMaTrangThaiCode=nv.iMaTrangThaiCode
                                   }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(laynhiemvuvualuu);
        }
        public ActionResult SuaNhiemVu(int id, string vTenNhiemVu, string vMoTa, DateTime dNgayBD, DateTime dNgayKT)
        {
            
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            Models.NhiemVu nhiemvu = new Models.NhiemVu();
            List<LayNhiemVu> laynhiemvuvualuu = new List<Models.LayNhiemVu>();
            try
            {
                nhiemvu = db.NhiemVus.Find(id);
                nhiemvu.vTenNhiemVu = vTenNhiemVu;
                nhiemvu.vMoTa = vMoTa;
                //nhiemvu.iMaNguoiDangCode = 1;
                nhiemvu.dNgayBD = dNgayBD;
                nhiemvu.dNgayKT = dNgayKT;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(true);
        }
        [ValidateInput(false)]
        public ActionResult LuuLoiCanSua(string noidungloi, int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            ChiTietLoi chitietloi = new ChiTietLoi();
            Models.NhiemVu nhiemvu = new Models.NhiemVu();
            try
            {
                nhiemvu = db.NhiemVus.Find(id);
                nhiemvu.iMaTrangThaiCode = 5;
                chitietloi.iMaNhiemVuCode = id;
                chitietloi.vChiTietLoi = noidungloi;
                chitietloi.iTrangThai = 1;
                db.ChiTietLois.Add(chitietloi);
                db.SaveChanges();
               
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return Json(chitietloi);
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
                        string[] extFile = new string[] { ".xls", ".xlsx", ".doc", ".docx", ".txt" };
                        using (var db = new dbnhiemvuEntities())
                        {
                            // kiem tra dinh ddng file là .jpg hoac .png
                            if (extHinh.Contains(Path.GetExtension(file.FileName).ToLower()))
                            {
                                var a = Path.GetExtension(file.FileName).ToLower();
                                // t?o thu m?c luu hình
                                string spDirPath = Server.MapPath("~/Content/Images");
                                string targetDirpath = Path.Combine(spDirPath, iMaNhiemVuCode.ToString());
                                Directory.CreateDirectory(targetDirpath);
                                // lay tên file
                                var fileName = Path.GetFileName(file.FileName);
                                // tao duong dan và luu hình anh
                                var path = Path.Combine(Server.MapPath($"~/Content/Images/{iMaNhiemVuCode}/"), fileName);
                                file.SaveAs(path);
                                // luu hình anh vào co so du lieu
                                var image = new Hinh();
                                image.vDuongDan = $"~/Content/Images/{iMaNhiemVuCode}/{fileName}";
                                image.iMaNhiemVuCode = iMaNhiemVuCode;
                                image.iTrangThai = 1;
                                db.Hinhs.Add(image);
                                db.SaveChanges();
                            }
                            else if (extFile.Contains(Path.GetExtension(file.FileName).ToLower()))
                            {
                                // tao thu muc luu file
                                var spDirPath = Server.MapPath("~/Content/Files");
                                var targetDirpath = Path.Combine(spDirPath, iMaNhiemVuCode.ToString());
                                Directory.CreateDirectory(targetDirpath);
                                // lay tên file
                                var fileName = Path.GetFileName(file.FileName);
                                // tao duong dan và luu hình anh
                                var path = Path.Combine(Server.MapPath($"~/Content/Files/{iMaNhiemVuCode}/"), fileName);
                                file.SaveAs(path);
                                // luu hình anh vào co so du leu
                                var taptin = new TapTin();
                                taptin.vTenTapTin = fileName;
                                taptin.vDuongDan = $"~/Content/Files/{iMaNhiemVuCode}/{fileName}";
                                taptin.iMaNhiemVuCode = iMaNhiemVuCode;
                                taptin.iTrangThai = 1;
                                db.TapTins.Add(taptin);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return true;
            }
            catch(DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public ActionResult UploadFileErro()
        {
            try
            {
                dbnhiemvuEntities db = new dbnhiemvuEntities();
                var hinhanh = new Hinh();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        string[] extHinh = new string[] { ".jpg", ".png" };
                       
                                // tao thu muc luu file
                                var spDirPath = Server.MapPath("~/Content/UpImageErro");
                                var targetDirpath = Path.Combine(spDirPath);
                                Directory.CreateDirectory(targetDirpath);
                                // lay tên file
                                var fileName = Path.GetFileName(file.FileName);
                                // tao duong dan và luu hình anh
                                var path = Path.Combine(Server.MapPath($"~/Content/UpImageErro/"), fileName);
                                file.SaveAs(path);
                                // luu hình anh vào co so du lieu
                                hinhanh.vDuongDan = $"Content/UpImageErro/{fileName}";
                                hinhanh.iTrangThai = 1;
                                db.Hinhs.Add(hinhanh);
                                db.SaveChanges();
                    }
                }
                return Json(hinhanh.vDuongDan);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public ActionResult NhanNhiemVu(int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            Models.NhiemVu nhiemvu = new Models.NhiemVu();
            try
            {
                nhiemvu = db.NhiemVus.Find(id);
                nhiemvu.iMaTrangThaiCode = 2;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(true);
        }
        public ActionResult NopNhiemVu(int id,int trangthai)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            Models.NhiemVu nhiemvu = new Models.NhiemVu();
            try
            {
                if(trangthai==2)
                {
                    nhiemvu = db.NhiemVus.Find(id);
                    nhiemvu.iMaTrangThaiCode = 3;
                    db.SaveChanges();
                }
                else if(trangthai==3)
                {
                    nhiemvu = db.NhiemVus.Find(id);
                    nhiemvu.iMaTrangThaiCode = 5;
                    db.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(true);
        }
        public ActionResult Duyet(int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            Models.NhiemVu nhiemvu = new Models.NhiemVu();
            try
            {
                    nhiemvu = db.NhiemVus.Find(id);
                    nhiemvu.iMaTrangThaiCode = 4;
                    db.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(true);
        }
        public ActionResult LayNhiemVu()
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<LayNhiemVu> nhiemvu = new List<LayNhiemVu>();
            try
            {
                nhiemvu = (from nv in db.NhiemVus
                           join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                           join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                           select new LayNhiemVu
                           {
                               iMaNhiemVuCode = nv.iMaNhiemVuCode,
                               vNguoiDang = tv.vTenDangNhap,
                               vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                               vMoTa = nv.vMoTa,
                               vTenNhiemVu = nv.vTenNhiemVu,
                               dNgayBD = nv.dNgayBD,
                               dNgayKT = nv.dNgayKT,
                               iMaTrangThaiCode = nv.iMaTrangThaiCode
                           }).ToList();
            }
            catch(DbEntityValidationException e)
            {
                throw e;
            }
            return Json(nhiemvu);
        }
        public ActionResult LayFile(int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<TapTin> taptin = new List<TapTin>();
            try
            {
                taptin = db.TapTins.Where(x => x.iMaNhiemVuCode == id).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(taptin);
        }
        public ActionResult LayFileCanTai(int id,int idmafile)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<TapTin> taptin = new List<TapTin>();
            try
            {
                taptin = db.TapTins.Where(x => x.iMaNhiemVuCode == id && x.iMaTapTinCode==idmafile).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(taptin);
        }
        [HttpGet]
        public virtual ActionResult Download(string file , int id)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Content/Files/"+ id + "/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        public ActionResult LayNhiemVuDangLam()
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<LayNhiemVu> nhiemvu = new List<LayNhiemVu>();
            try
            {
                nhiemvu = (from nv in db.NhiemVus
                           join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                           join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                           where tv1.iMaThanhVienCode==2 && nv.iMaTrangThaiCode==2
                           select new LayNhiemVu
                           {
                               iMaNhiemVuCode = nv.iMaNhiemVuCode,
                               vNguoiDang = tv.vTenDangNhap,
                               vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                               vMoTa = nv.vMoTa,
                               vTenNhiemVu = nv.vTenNhiemVu,
                               dNgayBD = nv.dNgayBD,
                               dNgayKT = nv.dNgayKT,
                               iMaTrangThaiCode = nv.iMaTrangThaiCode
                           }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(nhiemvu);
        }
        public ActionResult LayNhiemVuDangChoDuyet()
        {
            var iMaNguoiDuocGiaoCode = CurrentContext.GetUser().iMaThanhVienCode;
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<LayNhiemVu> nhiemvu = new List<LayNhiemVu>();
            try
            {
                nhiemvu = (from nv in db.NhiemVus
                           join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                           join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                           where tv1.iMaThanhVienCode == iMaNguoiDuocGiaoCode && nv.iMaTrangThaiCode == 3
                           select new LayNhiemVu
                           {
                               iMaNhiemVuCode = nv.iMaNhiemVuCode,
                               vNguoiDang = tv.vTenDangNhap,
                               vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                               vMoTa = nv.vMoTa,
                               vTenNhiemVu = nv.vTenNhiemVu,
                               dNgayBD = nv.dNgayBD,
                               dNgayKT = nv.dNgayKT,
                               iMaTrangThaiCode = nv.iMaTrangThaiCode
                           }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(nhiemvu);
        }
        public ActionResult LayNhiemVuDaHoanThanh(int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<LayNhiemVu> nhiemvu = new List<LayNhiemVu>();
            try
            {
                nhiemvu = (from nv in db.NhiemVus
                           join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                           join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                           where tv1.iMaThanhVienCode == id && nv.iMaTrangThaiCode == 4
                           select new LayNhiemVu
                           {
                               iMaNhiemVuCode = nv.iMaNhiemVuCode,
                               vNguoiDang = tv.vTenDangNhap,
                               vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                               vMoTa = nv.vMoTa,
                               vTenNhiemVu = nv.vTenNhiemVu,
                               dNgayBD = nv.dNgayBD,
                               dNgayKT = nv.dNgayKT,
                               iMaTrangThaiCode = nv.iMaTrangThaiCode
                           }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(nhiemvu);
        }
        public ActionResult LayNhiemVuCanSua()
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<LayNhiemVu> nhiemvu = new List<LayNhiemVu>();
            try
            {
                nhiemvu = (from nv in db.NhiemVus
                           join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                           join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                           where nv.iMaTrangThaiCode == 5
                           select new LayNhiemVu
                           {
                               iMaNhiemVuCode = nv.iMaNhiemVuCode,
                               vNguoiDang = tv.vTenDangNhap,
                               vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                               vMoTa = nv.vMoTa,
                               vTenNhiemVu = nv.vTenNhiemVu,
                               dNgayBD = nv.dNgayBD,
                               dNgayKT = nv.dNgayKT,
                               iMaTrangThaiCode = nv.iMaTrangThaiCode
                           }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(nhiemvu);
        }
        public ActionResult LayDanhSachLoi(int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<LayDanhSachLoi> danhsachloi = new List<LayDanhSachLoi>();
            try
            {
                danhsachloi = (from nv in db.NhiemVus
                           join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                           join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                           join loi in db.ChiTietLois on nv.iMaNhiemVuCode equals loi.iMaNhiemVuCode
                           where nv.iMaTrangThaiCode == 5 && loi.iMaNhiemVuCode==id
                           select new LayDanhSachLoi
                           {
                               iMaNhiemVuCode = nv.iMaNhiemVuCode,
                               vNguoiDang = tv.vTenDangNhap,
                               vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                               vMoTa = nv.vMoTa,
                               vTenNhiemVu = nv.vTenNhiemVu,
                               iMaTrangThaiCode = nv.iMaTrangThaiCode
                           }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(danhsachloi);
        }
        public ActionResult LayDanhSachNhiemVuduocGiao(int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<LayDanhSachLoi> danhsachnhiemvuduocgiao = new List<LayDanhSachLoi>();
            try
            {
                danhsachnhiemvuduocgiao = (from nv in db.NhiemVus
                               join tv in db.ThanhViens on nv.iMaNguoiDangCode equals tv.iMaThanhVienCode
                               join tv1 in db.ThanhViens on nv.iMaNguoiDuocGiaoCode equals tv1.iMaThanhVienCode
                               where nv.iMaTrangThaiCode == 1 && nv.iMaNguoiDuocGiaoCode==id
                               select new LayDanhSachLoi
                               {
                                   iMaNhiemVuCode = nv.iMaNhiemVuCode,
                                   vNguoiDang = tv.vTenDangNhap,
                                   vNguoiDuocGiaoCode = tv1.vTenDangNhap,
                                   vMoTa = nv.vMoTa,
                                   vTenNhiemVu = nv.vTenNhiemVu,
                                   iMaTrangThaiCode = nv.iMaTrangThaiCode
                               }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return Json(danhsachnhiemvuduocgiao);
        }
        public ActionResult ChiTietLoi(int id)
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            List<ChiTietLoi> chitietloi = new List<ChiTietLoi>();
            try
            { 
                chitietloi = db.ChiTietLois.Where(x => x.iMaNhiemVuCode == id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(chitietloi);
        }
        public ActionResult LayThongTinThanhVien()
        {
                dbnhiemvuEntities db = new dbnhiemvuEntities();
                List<ThongTinThanhVien> thongtinthanhvien = new List<ThongTinThanhVien>();
                List<ThongTinThanhVien> thongtinthanhvien1 = new List<ThongTinThanhVien>();
                List<LayDanhSachLoi> danhsachloi = new List<LayDanhSachLoi>();
                List<ThanhVien> thanhvien = new List<ThanhVien>();
            try
            {
                
                thanhvien = db.ThanhViens.Where(x => x.iTrangThai == 1).ToList();
                var nvloi = db.NhiemVus.Where(m => m.iMaTrangThaiCode == 5).Count();
                int nhiemvudahoanthanh=0; 
                int nhiemvuduocgiao=0; 
                int hoatdong=0;
                foreach (var item in thanhvien)
                {
                    nvloi = db.NhiemVus.Where(m => m.iMaTrangThaiCode == 5 && m.iMaNguoiDuocGiaoCode == item.iMaThanhVienCode).Count();
                    nhiemvudahoanthanh = db.NhiemVus.Where(x => x.iMaNguoiDuocGiaoCode == item.iMaThanhVienCode && x.iMaTrangThaiCode == 4).Count();
                    nhiemvuduocgiao = db.NhiemVus.Where(x => x.iMaNguoiDuocGiaoCode == item.iMaThanhVienCode).Count();
                    hoatdong = db.NhiemVus.Where(x => x.iMaNguoiDuocGiaoCode == item.iMaThanhVienCode && x.iMaTrangThaiCode == 2).Count();
                    thongtinthanhvien1.Add(new ThongTinThanhVien
                    {
                        iMaThanhVienCode = item.iMaThanhVienCode,
                        vTenDangNhap = item.vTenDangNhap,
                        iSoNhiemVuDaHoanThanh = nhiemvudahoanthanh,
                        iSoNhiemVuDuocGiao = nhiemvuduocgiao,
                        iSoLoiPhaiSua = nvloi,
                        iHoatDong = hoatdong,
                    });
                }
                return Json(thongtinthanhvien1);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
       
        }
        public ActionResult NhiemVuDangLam()
        {
            return View();
        }
        public ActionResult NhiemVuDangChoXacNhan()
        {
            return View();
        }
        public ActionResult NhiemVuDaHoanThanh()
        {
            return View();
        }
        public ActionResult NhiemVuCanSua()
        {
            return View();
        }
        public ActionResult ThanhVien()
        {
            dbnhiemvuEntities db = new dbnhiemvuEntities();
            ViewBag.ListNguoiDuocGiaoTask = db.ThanhViens.ToList();
            return View();
        }
    }
}