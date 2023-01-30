using Azure;
using InsourceData.AuthHelper;
using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Models.ViewModel.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InsourceSupport.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAuthenticationRepository _authRepo;
        private readonly ICryptoMD5 _cryptoMD5;
        private readonly ILookupRepository _lookupRepo;

        public UserController(IAuthenticationRepository authRepo, ICryptoMD5 cryptoMD5, ILookupRepository lookupRepo)
        {
            _authRepo = authRepo;
            _cryptoMD5 = cryptoMD5;
            _lookupRepo = lookupRepo;
        }

        public async Task<IActionResult> RegisterUser()
        {

            return View();
        }

        public async Task<IActionResult> getRoleList()
        {
            return Ok(await _lookupRepo.GetRoleList());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel details)
        {
            
            if (ModelState.IsValid)
            {
                details.Salt = RandomStringGenerator.Generate(6);
                details.Password =_cryptoMD5.MD5Hash(details.Password+details.Salt);
                details.JoinedDate= DateTime.UtcNow;
                var response = await _authRepo.RegisterUser(details);
                if (response.HasError == false)
                {
                    TempData["success"] = "User successfully registered";
                    return Ok();
                }
                else
                {
                    ViewBag.errorMessage = "Opps Something went wrong";
                    return View("RegisterUser", details);
                }
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(new DbResponse() { HasError = true, Message = "Registration failed. Something went wrong" });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
