using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.DTO.Responses
{
    public class OrderDto
    {
        public int Id { get; set; }
        public StatusOrderEnum StatusOrder { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? UserFullName { get; set; }   
        public List<OrderItemDto> Items { get; set; }
    }
}
