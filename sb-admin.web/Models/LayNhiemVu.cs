using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sb_admin.web.Models;
namespace sb_admin.web.Models
{
    public class LayNhiemVu
    {
        public int iMaNhiemVuCode { get; set; }
        public string vTenNhiemVu { get; set; }
        public string vTenTrangThai { get; set; }
        public string vMoTa { get; set; }
        public Nullable<System.DateTime> dNgayBD { get; set; }
        public Nullable<System.DateTime> dNgayKT { get; set; }
        public Nullable<System.DateTime> dNgayLap { get; set; }
        public string vNguoiDang { get; set; }
        public string vNguoiDuocGiaoCode { get; set; }
        public int? iMaTrangThaiCode { get; set; }
    }
}