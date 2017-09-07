using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin.web.Models
{
    public class GetThanhVienViewModel
    {
        public int iMaThanhVienCode { get; set; }
        public string vTenDangNhap { get; set; }
    }
    public class NhiemVuThanhVienViewModel
    {
        public string vTenDangNhap { get; set; }
        public int iMaThanhVienCode { get; set; }
        public int iSoNhiemVuMoi { get; set; }
        public int iSoNhiemVuDaHoanThanh { get; set; }
        public int iSoNhiemVuDangThucHien { get; set; }
        public int iSoNhiemVuDangChoDuyet { get; set; }
        public int iSoNhiemVuDangSuaLoi { get; set; }
        public int iSoNhiemVuDuocGiao { get; set; }
    }
    public class ChiTietNhiemVuViewModel
    {
        
        public int iMaNhiemVuCode { get; set; }
        public string vTenNhiemVu { get; set; }
        public string vMoTa { get; set; }
        public string vTenNguoiDang { get; set; }
        public string vTenNguoiDuocGiao { get; set; }
        public Nullable<System.DateTime> dNgayBD { get; set; }
        public Nullable<System.DateTime> dNgayKT { get; set; }
        public Nullable<int> iMaTrangThaiCode { get; set; }
        public Nullable<int> iMaNguoiDangCode { get; set; }
        public Nullable<int> iMaNguoiDuocGiaoCode { get; set; }
        public string vTenTrangThai { get; set; }
        public Nullable<System.DateTime> dNgayLap { get; set; }
    }
    public class ThemNhiemVuMoel
    {
        public Nullable<int> iMaModuleCode { get; set; }
        public string vTenNhiemVu { get; set; }
        public string vMoTa { get; set; }
        public Nullable<System.DateTime> dNgayBD { get; set; }
        public Nullable<System.DateTime> dNgayKT { get; set; }
        public Nullable<int> iMaNguoiDuocGiaoCode { get; set; }
    }
    public class BaoCaoNhiemVuViewModel
    {
        public int iMaNhiemVuCode { get; set; }
        public string vMoTa { get; set; }
        public string vDuongDanTapTin { get; set; }
    }
    public class ThemLoiViewModel {
        public Nullable<int> iMaNhiemVuCode { get; set; }
        public string vTenLoi { get; set; }
        public string vChiTietLoi { get; set; }
    }
}