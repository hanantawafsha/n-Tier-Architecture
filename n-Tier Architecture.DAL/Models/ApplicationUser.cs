using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string? CodeResetPassword { get; set; }
        public int? AddressId { get; set; }  
        public Address? Address { get; set; }
        public DateTime?   PasswordResetCodeExpire { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();



    }
}
