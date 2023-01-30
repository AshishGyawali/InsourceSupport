using Microsoft.AspNetCore.Mvc;
using InsourceData.Models.ViewModel.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using InsourceData.Interface;
using InsourceData.Repository.Auth;

namespace InsourceSupport.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationRepository _authenticationRepo;
        private readonly ICryptoMD5 _cryptoMD5;
        private readonly ILookupRepository _lookupRepo;

        public AuthController(IAuthenticationRepository authenticationRepo, ICryptoMD5 cryptoMD5, ILookupRepository lookupRepo)
        {
            _authenticationRepo = authenticationRepo;
            _cryptoMD5 = cryptoMD5;
            _lookupRepo = lookupRepo;
        }

        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("UserName,Password,RememberMe")] LoginViewModel details)
        {
            if (ModelState.IsValid)
            {
                var user = await _authenticationRepo.GetCredentials(details);
                if (user != null && user.Password == _cryptoMD5.MD5Hash(details.Password+user.Salt))
                {
                    var claims = new List<Claim>
                        {
                            new Claim("UserId",user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.FullName),
                            new Claim("UserName", user.UserName),
                            new Claim(ClaimTypes.Role, user.Role),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("ContactNumber", user.ContactNumber),
                            new Claim("JoinedDate", user.JoinedDate.ToString()),
                            new Claim("IsSystemUser", user.IsSystemUser.ToString()),

                        };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {

                    };
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    TempData["success"] = "Successfully logged in";
                    return Redirect("/Admin/Home/Index");
                    //return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
            }
            TempData["error"] = "Username or password incorrect";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
