using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sb_admin.web.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "sssssssssss.")]
        [Display(Name = "TaiKhoan")]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "asasasas.")]
        [DataType(DataType.Password)]
        [Display(Name = "MatKhau")]
        public string MatKhau { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public int id { get; set; }
        public Nullable<int> Quyen { get; set; }
        public HttpContextBase HttpContext { get; }
    }
}