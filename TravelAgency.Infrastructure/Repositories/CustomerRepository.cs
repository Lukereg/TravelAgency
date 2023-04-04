using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Customer>> GetAll()
        {
            List<Customer> customers = new();

            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                customers = await dbContext.Customers.ToListAsync();
            }

            return customers;
        }

        public async Task<Customer?> GetById(Guid id)
        {
            Customer customer = new();

            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            }
            return customer;
        }
    }
}
