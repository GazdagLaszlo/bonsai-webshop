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
    public interface IProductCategoryService
    {
        Task<ICollection<ProductCategoryDTO>> GetAllAsync();
        Task<ProductCategoryDTO?> GetByUrlSlugAsync(string urlSlug);
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<ProductCategoryDTO?>> GetAllAsync()
        {
            var categories = await _context.ProductCategories.ToListAsync();

            return _mapper.Map<ICollection<ProductCategoryDTO>>(categories);
        }

        public async Task<ProductCategoryDTO?> GetByUrlSlugAsync(string urlSlug)
        {
            var category = await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.UrlSlug == urlSlug);
            return _mapper.Map<ProductCategoryDTO?>(category);
        }
    }
}
