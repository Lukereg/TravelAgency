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

        public async Task Update(Order order, Guid id)
        {
            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                var orderDb = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

                if (orderDb != null)
                {
                    orderDb.TourName = order.TourName;
                    orderDb.Description = order.Description;
                    orderDb.StartDate = order.StartDate;
                    orderDb.EndDate = order.EndDate;
                    orderDb.Price= order.Price;

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
