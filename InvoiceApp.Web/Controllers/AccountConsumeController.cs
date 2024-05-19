using InvoiceApp.Core.DTOs.Authentication;
using InvoiceApp.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace InvoiceApp.Web.Controllers
{
    public class AccountConsumeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountConsumeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public IActionResult LoginAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAccount(LoginDto loginDto)
        {
            var client = _clientFactory.CreateClient("BaseApiUrl");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Account/Login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthenticationDto>(responseContent);

                if (result.IsAuthenticated)
                {
                    // Store the token in cookies
                    HttpContext.Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = result.ExpiresOn
                    });

                    // Store user info in session
                    HttpContext.Session.SetString("Email", loginDto.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, result.Message);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while logging in.");
            }

            return View(loginDto);
        }

        [HttpGet]
        public IActionResult RegisterAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccount(RegisterDto registerDto)
        {
            var client = _clientFactory.CreateClient("BaseApiUrl");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Account/Register", registerDto);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthenticationDto>(responseContent);

                if (result.IsAuthenticated)
                {
                    // Store the token in cookies
                    HttpContext.Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = result.ExpiresOn
                    });

                    // Store user info in session
                    HttpContext.Session.SetString("FirstName", registerDto.FirstName);
                    HttpContext.Session.SetString("LastName", registerDto.LastName);
                    HttpContext.Session.SetString("Email", registerDto.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, result.Message);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while registering.");
            }

            return View(registerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Clear the cookies
            HttpContext.Response.Cookies.Delete("AuthToken");

            // Clear the session
            HttpContext.Session.Clear();

            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

    }
}
