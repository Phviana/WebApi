using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.DAL.Security
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
