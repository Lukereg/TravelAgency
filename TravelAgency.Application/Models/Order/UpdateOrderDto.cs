using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Application.Models.Order
{
    public class UpdateOrderDto
    {
        public string TourName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
