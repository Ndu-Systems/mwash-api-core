using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreedomCode.Api.Data
{
    // joining table between user [staff, lead, etc] and App variable type [staff, lead, admin etc.]
    public class UserAppVariableType
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AppVariableTypeId { get; set; }
        public AppVariableType AppVaribleType { get; set; }
    }
}
