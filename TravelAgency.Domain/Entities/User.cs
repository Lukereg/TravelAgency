using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        public List<Order> CreatedOrders { get; set; } = new();
    }
}
