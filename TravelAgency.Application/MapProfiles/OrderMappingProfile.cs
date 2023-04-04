using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.Models.Customer;
using TravelAgency.Application.Models.Order;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.MapProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<AddOrderDto, Order>();
            CreateMap<Order, GetOrderDto>();
            CreateMap<UpdateOrderDto, Order>();
        }
    }
}
