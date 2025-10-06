using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.DTO.Requests
{
    public class OrderItemRequestDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
