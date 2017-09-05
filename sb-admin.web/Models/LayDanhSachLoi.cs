using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin.web.Models
{
    public class LayDanhSachLoi
    {
        public int iMaNhiemVuCode { get; set; }
        public string vTenNhiemVu { get; set; }
        public string vMoTa { get; set; }
        public Nullable<System.DateTime> dNgayBD { get; set; }
        public Nullable<System.DateTime> dNgayKT { get; set; }
        public string vNguoiDang { get; set; }
        public string vNguoiDuocGiaoCode { get; set; }
        public int? iMaTrangThaiCode { get; set; }
        public string vChiTietLoi { get; set; }
        public int iTrangThai { get; set; }
    }
}