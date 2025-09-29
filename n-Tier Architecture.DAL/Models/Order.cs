using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Models
{
   public enum StatusOrderEnum
    {
        Pending = 0,
        Canceled = 1, 
        Approved = 2,
        Shipped = 3,
        Delivered = 4
    }
    public enum PaymnetMethodEnum
    {
        Cash = 0,
        Visa = 1
    }
    public class Order:BaseModel
    {
        public StatusOrderEnum StatusOrder { get; set; } = StatusOrderEnum.Pending;
        public DateTime ShippedDate { get; set; }
        public decimal TotalPrice { get; set; }
   //payment
   public PaymnetMethodEnum PaymnetMethod { get; set; }
        public string? PaymentId { get; set; }
        //public string? SessionId { get; set;}
       // public string? TransactionId { get; set; }

        public string? CarrierName { get; set; }
        public string? TrackingNbr { get; set; }

        //user info - relation
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        //order items 
        public List<OrderItem> OrderItems { get; set; }

    }
}
