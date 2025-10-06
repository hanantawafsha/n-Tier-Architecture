using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.DTO.Responses
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }
        public string MainImageUrl {  get; set; }
        //=> $"https://localhost:7084/images/{MainImage}";
        public List<string> SubImagesUrl { get; set; } = new List<string>();

        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
    }
}
