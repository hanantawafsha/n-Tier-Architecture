using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Models
{
    public class Review:BaseModel
    {

        public int Rate { get; set; }
        public string? Comment { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        //number of orders
        public int Ordering {  get; set; }

    }
}
