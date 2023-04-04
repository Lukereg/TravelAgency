using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.Models.Order;

namespace TravelAgency.Application.Services.OrderService
{
    public interface IOrderService
    {
        Task AddOrder(AddOrderDto addOrderDto);
    }
}
