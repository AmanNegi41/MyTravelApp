using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTravelApp.Data.Models
{
    public class LoginUser
    {
        public string UserId { get; set; }
        public string PasswordHash { get; set; }
    }
}
