using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Models.ViewModel.Auth
{
    public class RegisterUserViewModel
    {

        public int Id { get; set; } = 0;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime JoinedDate { get; set; }
        public int RoleId { get; set; }
        public bool IsSystemUser { get; set; }

    }
}
