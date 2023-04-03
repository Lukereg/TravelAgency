using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.Models.User;

namespace TravelAgency.Application.Services.UserService
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task<bool> LoginUser(LoginUserDto loginUserDto);
    }
}
