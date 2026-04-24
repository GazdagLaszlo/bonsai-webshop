using AutoMapper;
using bonsai_webshop.DataContext.Context;
using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductDTO>> GetAllAsync(string? urlSlug = null);
    }
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ICollection<ProductDTO>> GetAllAsync(string? urlSlug = null)
        {            

            var query = _context.Products
                .Include(p => p.ProductCategory)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(urlSlug))
            {
                query = query.Where(x => x.ProductCategory != null &&
                                         x.ProductCategory.UrlSlug == urlSlug);
            }

            var products = await query.ToListAsync();
            return _mapper.Map<ICollection<ProductDTO>>(products);
        }
    }
}
