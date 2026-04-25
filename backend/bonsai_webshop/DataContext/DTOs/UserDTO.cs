using bonsai_webshop.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.DataContext.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<AddressDTO> Addresses { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
    public class UserCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
    }
}
