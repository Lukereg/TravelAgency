using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Persistence;

namespace TravelAgency.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task Add(User user)
        {
            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            List<User> users = new();

            using (TravelAgencyDbContext dbContext = new TravelAgencyDbContext())
            {
                users = await dbContext.Users.ToListAsync();
            }

            return users;
        }
    }
}
