using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.DataContext.Entities
{
    public class User
    {
        public int Id { get; set; }       
        public string Name { get; set; }        
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
