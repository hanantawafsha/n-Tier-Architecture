using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.DTO.Requests
{
    public class BrandRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile MainImage { get; set; }


    }
}
