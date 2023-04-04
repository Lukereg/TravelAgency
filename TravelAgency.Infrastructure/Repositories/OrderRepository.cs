using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Persistence;

namespace TravelAgency.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task Add(Order order)
        {
            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                await dbContext.Orders.AddAsync(order);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

                if (order != null)
                {
                    dbContext.Orders.Remove(order);
                    await dbContext.SaveChangesAsync();
                }           
            }
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            List<Order> orders = new();

            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                orders = await dbContext.Orders.ToListAsync();
            }

            return orders;
        }

    }
}
