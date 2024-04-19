using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3.Models.Model_View
{
    public class DoiMatKhau
    {
        
        [Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống mật khẩu mới")]
        [MaxLength(30, ErrorMessage = "Mật khẩu có độ dài từ 8 đến 30 ký tự")]
        [MinLength(8, ErrorMessage = "Mật khẩu có độ dài từ 8 đến 30 ký tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống ")]
        [Compare("Password", ErrorMessage = "Không trùng với mật khẩu mới")]
        public string ConfirmPassword { get; set; }
        
    }
}