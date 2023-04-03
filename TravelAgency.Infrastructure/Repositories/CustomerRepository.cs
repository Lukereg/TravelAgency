using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Persistence;

namespace TravelAgency.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task Add(Customer customer)
        {
            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                await dbContext.Customers.AddAsync(customer);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
