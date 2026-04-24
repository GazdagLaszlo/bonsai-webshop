using AutoMapper;
using bonsai_webshop.DataContext.Context;
using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateAsync(OrderCreateDTO dto, int userId);
    }
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrderDTO> CreateAsync(OrderCreateDTO dto, int userId)
        {
            if (dto.OrderItems == null || !dto.OrderItems.Any())
                throw new Exception("Order must contain at least one item");

            var order = new Order
            {
                OrderDate = DateTime.Now,
                Status = OrderStatus.Created,
                UserId = userId,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in dto.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);

                if (product == null)
                    throw new Exception($"Product with id - {item.ProductId} not found");

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            order.TotalAmount = order.OrderItems
                .Sum(x => x.Quantity * x.UnitPrice);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(order);

        }
    }
}
