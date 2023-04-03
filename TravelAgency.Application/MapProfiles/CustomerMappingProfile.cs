using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.Models.Customer;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.MapProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<AddCustomerDto, Customer>();
        }
    }
}
