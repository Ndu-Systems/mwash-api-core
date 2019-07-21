using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mwashi.api.Data.Models
{
    public class LoggedInUserModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public string Role { get; set; }

    }
}
