using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Models
{
    public class ProductImage:BaseModel
    {
        public string ImageName { get; set; }
        public int productId { get; set; }
        public Product Product { get; set; }

    }
}
