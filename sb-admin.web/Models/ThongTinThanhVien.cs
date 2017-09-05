using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin.web.Models
{
    public class ThongTinThanhVien
    {
        public string vTenDangNhap { get; set; }
        public int iSoNhiemVuDaHoanThanh { get; set; }
        public int iSoNhiemVuDuocGiao { get; set; }
        public int iSoLoiPhaiSua { get; set; }
        public int iHoatDong { get; set; }
        public int iMaThanhVienCode { get; set; }
    }

}