using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Application.Models.User
{
    public class LoginUserDto
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
