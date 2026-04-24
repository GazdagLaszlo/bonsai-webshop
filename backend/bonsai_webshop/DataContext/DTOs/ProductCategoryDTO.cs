using bonsai_webshop.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.DataContext.DTOs
{
    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProductCategoryType Type { get; set; }
        public string? Image { get; set; }
        public string UrlSlug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
