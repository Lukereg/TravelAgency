using AutoMapper;
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
        private List<IObserver> _observers = new List<IObserver>();


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
            Notify();
        }

        public async Task DeleteOrder(Guid id)
        {
            await _orderRepository.Delete(id);
            Notify();
        }

        public async Task<IEnumerable<GetOrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAll();
            var result = _orderMapper.Map<List<GetOrderDto>>(orders);

            return result;
        }

        public async Task UpdateOrder(UpdateOrderDto updateOrderDto, Guid id)
        {
            var order = _orderMapper.Map<Order>(updateOrderDto);
            await _orderRepository.Update(order, id);
            Notify();
        }

        public void Notify()
        {
            foreach (var observer in _observers)
                observer.Update();    
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}
