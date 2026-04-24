using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.DataContext.Entities
{    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public int? ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
    }
}
