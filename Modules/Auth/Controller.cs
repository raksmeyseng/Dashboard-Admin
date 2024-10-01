using System.Security.Claims;
using ArchtistStudio.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchtistStudio.Modules.Auth
{
    public class AuthController(
        IMapper mapper,
        IAuthRepository repository
    ) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(InsertLoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var admin = repository.FindBy(e => e.DeletedAt == null && e.Email == request.Email).FirstOrDefault();
                if (admin != null)
                {
                    bool isValid = admin.Password == request.Password;
                    if (isValid)
                    {
                        var claims = new List<Claim>
                {
                     new(ClaimTypes.Name, admin.Name),
                    new(ClaimTypes.NameIdentifier, admin.Id.ToString())
                };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                            new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                            });


                        HttpContext.Session.SetString("Email", admin.Email);
                        return RedirectToAction("gets", "project");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Not Found");
                    return View(request);
                }
            }

            return View(request);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(InsertRegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var existingUser = repository.FindBy(e => e.Email == request.Email).FirstOrDefault();
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already in use");
                    return View(request);
                }

                var item = mapper.Map<Auth>(request);
                repository.Add(item);
                repository.Commit();

                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, item.Email) },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString("Email", item.Email);
                   return RedirectToAction("gets", "project");
            }

            return View(request);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookie = Request.Cookies.Keys;
            foreach (var cookie in storedCookie)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
