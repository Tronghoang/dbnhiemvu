using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin.web.Models
{
    public class ThemProjectViewModel
    {
        public string vTenProject { get; set; }
        public string vMoTa { get; set; }
    }
    public class ChinhSuaProjectViewModel
    {
        public int iMaProjectCode { get; set; }
        public string vTenProject { get; set; }
        public string vMoTa { get; set; }
    }
    public class ProjectViewModel
    {
        public int iMaProjectCode { get; set; }
        public string vTenProject { get; set; }
        public string vMoTa { get; set; }
        public string dNgayLap { get; set; }
    }
}