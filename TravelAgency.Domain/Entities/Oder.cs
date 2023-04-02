using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string TourName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Customer Customer { get; set; } = default!;
        public Guid CustomerId { get; set; }
        public User Creator { get; set; } = default!;
        public Guid CreatorId { get; set; }
    }
}
