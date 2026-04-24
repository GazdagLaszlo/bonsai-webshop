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
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(UserCreateDTO userCreateDto);
    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDTO> CreateAsync(UserCreateDTO userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }
    }
}
