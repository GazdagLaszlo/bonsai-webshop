using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.DataContext.Entities
{
    public enum OrderStatus
    {
        Created,
        Pending,
        InProgress,
        Shipped,
        Delivered,
        Cancelled,
        Failed
    }
    public class Order
    {
        public int Id { get; set; }        
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount {  get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }
}
