using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin.web.Models
{
    public class ModuleViewModel
    {
        public int iMaModuleCode { get; set; }
        public string vTenModule { get; set; }
        public string vMoTa { get; set; }
        public string dNgayLap { get; set; }
    }
    public class ThemModuleViewModel
    {
        public Nullable<int> iMaProjectCode { get; set; }
        public string vTenModule { get; set; }
        public string vMoTa { get; set; }
    }
    public class ChinhSuaModuleViewModel
    {
        public int iMaModuleCode { get; set; }
        public string vTenModule { get; set; }
        public string vMoTa { get; set; }
    }
}