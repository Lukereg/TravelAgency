using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.Models.User;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.Services.UserService
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task<User?> LoginUser(LoginUserDto loginUserDto);
    }
}
