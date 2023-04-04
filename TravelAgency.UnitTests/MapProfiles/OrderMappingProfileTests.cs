using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.MapProfiles;
using TravelAgency.Application.Models.Order;
using TravelAgency.Domain.Entities;

namespace TravelAgency.UnitTests.MapProfiles
{
    public class OrderMappingProfileTests
    {
        private readonly IMapper _mapper;

        public OrderMappingProfileTests() 
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<OrderMappingProfile>());
            _mapper = configuration.CreateMapper();
        }

        [Theory]
        [InlineData("Testname", "Testdescription", 123456.24, "2015-05-16T05:50:06", "2015-06-18T05:50:06")]
        [InlineData("Test", "Descript", 1234, "2023-05-16T05:50:06", "2024-06-18T05:50:06")]
        public void CreateMap_AddOrderDtoToOrder_MapsCorrectly(String tourName, String description, Decimal price, DateTime startDate, DateTime endDate)
        {
            //arrange
            var addOrderDto = new AddOrderDto
            {
                TourName = tourName,
                Description = description,
                Price = price,
                StartDate = startDate,
                EndDate = endDate
            };

            //act 
            var order = _mapper.Map<Order>(addOrderDto);

            //assert
            order.TourName.Should().Be(addOrderDto.TourName);
            order.Description.Should().Be(addOrderDto.Description);
            order.Price.Should().Be(addOrderDto.Price);
            order.StartDate.Should().Be(addOrderDto.StartDate);
            order.EndDate.Should().Be(addOrderDto.EndDate);
        }
    }
}
