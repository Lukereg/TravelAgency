using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Presentation
{
    public static class AppSettings
    {
        public static Guid UserLoggedInId { get; set; } = Guid.Empty;
        public static bool IsUserLoggedIn { get; set; } = false;
    }
}
