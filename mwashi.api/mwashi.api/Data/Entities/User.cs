using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mwashi.api.Data.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyUserId { get; set; }
        public int StatusId { get; set; }
    }
}
