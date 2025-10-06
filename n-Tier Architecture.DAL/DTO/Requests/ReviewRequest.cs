using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.DTO.Requests
{
    public class ReviewRequest
    {
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public int ProductId { get; set; }
    }
}
