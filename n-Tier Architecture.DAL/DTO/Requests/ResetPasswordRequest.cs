using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.DTO.Requests
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
