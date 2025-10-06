using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.DTO.Requests
{
    public class OrderRequestDto
    {
        public string UserId { get; set; } = null!;
        public List<OrderItemRequestDto> Items { get; set; } = new List<OrderItemRequestDto>();
    }
}
