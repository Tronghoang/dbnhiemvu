using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhiemVu.Models
{
    public class BaoCaoNhiemVuViewModel {
        public int iMaNhiemVuCode { get; set; }
        public string vMoTa { get; set; }
        public string vDuongDanTapTin { get; set; }
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
        public string vTenTrangThai { get; set; }
        public Nullable<System.DateTime> dNgayLap { get; set; }
    }
    public class GetThanhVienViewModel
    {
        public int iMaThanhVienCode { get; set; }
        public string vTenDangNhap { get; set; }
    }
    public class GetBangNhiemVuViewModel
    {
        public int iMaNhiemVuCode { get; set; }
        public string vTenNhiemVu { get; set; }
        public Nullable<System.DateTime> dNgayBD { get; set; }
        public Nullable<System.DateTime> dNgayKT { get; set; }
        public string vNguoiDuocGiao { get; set; }
        public Nullable<int> iMaTrangThaiCode { get; set; }
        public string vTenTrangThai { get; set; }
        public Nullable<System.DateTime> dNgayLap { get; set; }
    }
    public class ThemNhiemVuMoel
    {
        public string vTenNhiemVu { get; set; }
        public string vMoTa { get; set; }
        public Nullable<System.DateTime> dNgayBD { get; set; }
        public Nullable<System.DateTime> dNgayKT { get; set; }
        public Nullable<int> iMaNguoiDuocGiaoCode { get; set; }
    }
    public class LoginViewModel
    {
        public string vTenDangNhap { get; set; }
        public string vMatKhau { get; set; }
    }
    public class RegisterViewModel
    {
        public string vTenDangNhap { get; set; }
        public string vMatKhau { get; set; }
    }
}