using Moq;
using TravelAgency.Application.Models.Order;
using TravelAgency.Application.Services.OrderService;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.UnitTests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests() 
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService();
        }

        [Fact]
        public async Task GetAllOrders_OrdersExist_ReturnListOfGetOrderDto()
        {
            //arrange
            var orders = new List<Order>
            {
                new Order {
                    Id = Guid.NewGuid(),
                    CreatorId = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    Description ="Test description",
                    Price=500,
                    StartDate=DateTime.Parse("2022-04-05 10:30:00"),
                    EndDate=DateTime.Parse("2022-05-02 11:30:00"),
                    TourName="Test name"
                },
                new Order {
                    Id = Guid.NewGuid(),
                    CreatorId = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    Description ="Description",
                    Price=500,
                    StartDate=DateTime.Parse("2022-03-05 10:30:00"),
                    EndDate=DateTime.Parse("2022-04-02 11:30:00"),
                    TourName="Name"
                }
            };

            _mockOrderRepository.Setup(x => x.GetAll()).ReturnsAsync(orders);

            //act
            var result = await _orderService.GetAllOrders();

            //assert
            Assert.IsType<List<GetOrderDto>>(result);
            Assert.Equal(orders.Count, result.Count());     
        }          
    }
}
