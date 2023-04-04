using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetById(Guid id);
    }
}
