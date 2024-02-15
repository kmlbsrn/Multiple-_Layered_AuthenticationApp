using System.Net;
using System.Security.Claims;
using Authentication.Business.Dtos;
using Authentication.Business.Services;
using Authentication.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebUI.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpFromViewModel formData)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ValidMessage = "Please correct the following errors.";
                return View(formData);
            }

            var userAddDto = new UserAddDto
            {
                Email = formData.Email.Trim(),
                FirstName = formData.FirstName.Trim(),
                LastName = formData.LastName.Trim(),
                Password = formData.Password
            };

            var result = _userService.AddUser(userAddDto);

            if (!result.IsSucceed)
            {

                TempData["SignUpMessage"] = result.Message;
                return View(formData);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ValidMessage = "Please correct the following errors.";
                return View(formData);
            }

            var signInDto = new SignInDto
            {
                Email = formData.Email,
                Password = formData.Password
            };

            var userInfo = _userService.SignInUser(signInDto);


            if (userInfo is null)
            {
                ViewBag.ErrorMessage = "Email or password is incorrect.";
                return View(formData);
            }

            var claims = new List<Claim>();

            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("firstName", userInfo.FirstName));
            claims.Add(new Claim("lastName", userInfo.LastName));
            claims.Add(new Claim("fullName", userInfo.FullName));


            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)),
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("SignIn", "Auth");
        }
    }
}
