using Admin.UI.Models;
using Admin.UI.Service;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Drawing.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Admin.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminService _adminService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IAdminService adminService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _adminService = adminService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("CarIndex", "Car"); // Redirect authenticated users to a different page
            }


            return View();
        }

        public async Task<ActionResult> AdminCreate()
        {
            return View();
        }

        public async Task<ActionResult> LoginIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("CarIndex", "Car"); // Redirect authenticated users to a different page
            }

            return View();
        }
        [HttpPost]

        public async Task<ActionResult> AdminLoginAsync(AdminDto model)
        {
            try
            {
                var (admin, token) = await _adminService.LoginAdmin(model);

                if (admin != null && !string.IsNullOrEmpty(token))
                {
                    // Sign in the user using the obtained token
                    await SignInAsync(token);

                    TempData["AdminLoginMessage"] = "Admin login successful.";
                    return RedirectToAction("CarIndex", "Car");
                }
                else
                {
                    TempData["dangerMessage"] = "Admin login failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                TempData["dangerMessage"] = "An error occurred while processing your request. Please try again.";
            }

            // If ModelState is not valid or the login fails, return to the view
            return View("LoginIndex", model);
        }


        private string GenerateSessionToken()
        {
            return Guid.NewGuid().ToString();
        }

        [HttpPost]
        public async Task<ActionResult> AdminCreateAsync(AdminDto model)
        {
            var adminList = await _adminService.GetAdminDetailsAsync();
            var existingAdmin = adminList.FirstOrDefault(e => e.UserName == model.UserName);

            if (existingAdmin == null)
            {
                if (ModelState.IsValid)
                {
                    var result = await _adminService.RegisterAdmin(model);

                    if (result != null)
                    {
                        TempData["successMessage"] = "Admin registration successful.";
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["dangerMessage"] = "Admin registration failed. Please try again.";
                }
            }
            else
            {
                // ModelState.AddModelError("UserName", "Admin with this username already exists.");
                TempData["dangerMessage"] = "Admin registration failed. Please try again.";
                return View("Index", model);
            }

            // If ModelState is not valid or the admin already exists, return to the view
            return View("Index", model);
        }



        public async Task<ActionResult> Logout()
        {
            // Expire the token cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("Token");

            // Sign out the user from the authentication system
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page or any other page as needed
            return RedirectToAction(nameof(LoginIndex));
        }

        private async Task SignInAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            // Check if the JWT token contains a claim for the 'Name' attribute
            var nameClaim = jwt.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
            if (nameClaim != null)
            {
                identity.AddClaim(new Claim(nameClaim.Type, nameClaim.Value));
            }
            else
            {
                // If 'Name' claim is missing, provide a default value or handle as needed
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, "Unknown"));
            }

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Log the error
            _logger.LogError("An error occurred: {RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
