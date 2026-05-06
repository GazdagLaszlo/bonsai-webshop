using bonsai_webshop.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.DataContext.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }       
        public string UrlSlug { get; set; }
        public ProductCategoryDTO ProductCategory { get; set; }
    }
}
