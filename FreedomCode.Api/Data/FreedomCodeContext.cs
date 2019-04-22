using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreedomCode.Api.Data
{
    public class FreedomCodeContext : DbContext
    {
        public FreedomCodeContext(DbContextOptions options)
            : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed a user

            modelBuilder.Entity<User>().HasData(
                  new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin@mail.com", Password = "admin", Role = Role.Admin },
                  new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user@mail.com", Password = "user", Role = Role.User }

                );

            #endregion 
        }
    }
}
