using InsourceData.DB;
using InsourceData.Models.Enquiry;
using InsourceData.Models.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Interface
{
    public interface IAuthenticationRepository
    {
        public Task<DbResponse> RegisterUser(RegisterUserViewModel credentials);
        public Task<LoginUserViewModel> GetCredentials(LoginViewModel credentials);
    }
}
