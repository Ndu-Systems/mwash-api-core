using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace mwashi.api.Data.Entities
{

    [Table("role")]
    public class Role
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyUserId { get; set; }
        public int StatusId { get; set; }
    }

    [Table("userrole")]
    public class UserRole
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyUserId { get; set; }
        public int StatusId { get; set; }
    }
}
