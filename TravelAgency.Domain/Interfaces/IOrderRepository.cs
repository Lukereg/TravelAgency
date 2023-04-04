using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<IEnumerable<Order>> GetAll();
    }
}
