using InvoiceApp.Core.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Core.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthenticationDto> LoginAsync(LoginDto loginDto);

    }
}
