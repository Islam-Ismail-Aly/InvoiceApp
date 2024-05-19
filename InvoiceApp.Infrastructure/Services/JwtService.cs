using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Infrastructure.Services
{
    public class JwtService
    {
        public string? Key { get; set; }
        public double DurationInDays { get; set; }
    }
}
