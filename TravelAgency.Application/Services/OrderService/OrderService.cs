﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.MapProfiles;
using TravelAgency.Application.Models.Customer;
using TravelAgency.Application.Models.Order;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Repositories;

namespace TravelAgency.Application.Services.OrderService
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _orderMapper;

        public OrderService() 
        {
            var confguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderMappingProfile>();
            });

            _orderMapper = confguration.CreateMapper();
            _orderRepository = new OrderRepository();
        }

        public async Task AddOrder(AddOrderDto addOrderDto)
        {
            var order = _orderMapper.Map<Order>(addOrderDto);
            await _orderRepository.Add(order);
        }
    }
}
