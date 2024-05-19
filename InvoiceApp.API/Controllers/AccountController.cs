using InvoiceApp.API.Utilities;
using InvoiceApp.Core.DTOs.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "AuthenticationAPIv1")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var result = await _authenticationService.LoginAsync(loginDto);

            if (!result.IsAuthenticated)
                return BadRequest(new { result.Message, result.IsAuthenticated });

            result.IsAuthenticated = true;

            return Ok(new APIResponseResult<AuthenticationDto>(result, "Login successfully."));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto model)
        {
            var result = await _authenticationService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return StatusCode(StatusCodes.Status400BadRequest, new APIResponseResult<JsonContent>(ModelState.ToString()));

            return Ok(new { token = result.Token, result.ExpiresOn });
        }
    }
}
