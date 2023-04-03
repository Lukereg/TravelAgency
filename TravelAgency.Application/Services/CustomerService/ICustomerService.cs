using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.Models.Customer;

namespace TravelAgency.Application.Services.CustomerService
{
    public interface ICustomerService
    {
        Task AddCustomer(AddCustomerDto addCustomerDto);
    }
}
