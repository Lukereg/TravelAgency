using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.MapProfiles;
using TravelAgency.Application.Models.Customer;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Repositories;

namespace TravelAgency.Application.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _customerMapper;

        public CustomerService()
        {
            var confguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerMappingProfile>();
            });

            _customerMapper = confguration.CreateMapper();
            _customerRepository = new CustomerRepository();
        }

        public async Task AddCustomer(AddCustomerDto addCustomerDto)
        {
            var customer = _customerMapper.Map<Customer>(addCustomerDto);
            await _customerRepository.Add(customer);
        }

        public async Task<IEnumerable<GetCustomerDto>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAll();
            var result = _customerMapper.Map<List<GetCustomerDto>>(customers);

            return result;
        }

        public async Task<GetCustomerDto> GetCustomerById(Guid id)
        {
            var customer = await _customerRepository.GetById(id);
            var result = _customerMapper.Map<GetCustomerDto>(customer);

            return result;
        }
    }
}
