using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Models.ViewModel.Auth
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Please enter valid username or email.")]
        [Display(Name = "Username/Email")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Salt { get; set; }

        public bool RememberMe { get; set; }
    }
}
