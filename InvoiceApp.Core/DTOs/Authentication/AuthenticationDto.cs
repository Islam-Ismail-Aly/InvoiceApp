using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Core.DTOs.Authentication
{
    public class AuthenticationDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
