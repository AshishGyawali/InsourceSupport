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

        public AuthController(IAuthenticationRepository authenticationRepo, ICryptoMD5 cryptoMD5)
        {
            _authenticationRepo = authenticationRepo;
            _cryptoMD5 = cryptoMD5;
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
                if (user != null && user.Password == _cryptoMD5.MD5Hash(details.Password))
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, "Ram"),
                            new Claim("FullName", "Ram Pd"),
                            new Claim(ClaimTypes.Role, "Administrator"),
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
                    return RedirectToAction("AdminPanel", "Home");
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
