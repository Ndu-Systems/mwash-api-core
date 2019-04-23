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
        public virtual DbSet<AppVariableType> AppVariableType { get; set; }
        public virtual DbSet<UserAppVariableType> UserAppVariableType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed methods

            modelBuilder.Entity<User>().HasData(
                  new User { UserId = 1, FirstName = "Admin", LastName = "User", Username = "admin@mail.com", Password = "admin"},
                  new User { UserId = 2, FirstName = "Normal", LastName = "User", Username = "user@mail.com", Password = "user"}

                );

            modelBuilder.Entity<AppVariableType>().HasData(
                        new AppVariableType
                        {
                            AppVariableTypeId = 1,
                            Name = "UserRole"
                        },
                        new AppVariableType
                        {
                            AppVariableTypeId = 1001,
                            Name = "Admin",
                            VariableTypeId = 1
                        },
                        new AppVariableType
                        {
                            AppVariableTypeId = 1002,
                            Name = "Staff",
                            VariableTypeId = 1
                        },
                        new AppVariableType
                        {
                            AppVariableTypeId = 1003,
                            Name = "Tanent",
                            VariableTypeId = 1
                        });

            modelBuilder.Entity<UserAppVariableType>().HasData(

                new UserAppVariableType
                {     
                    Id =100,
                    UserId = 1,
                    AppVariableTypeId = 1001
                },new UserAppVariableType
                {
                    Id = 1001,
                    UserId = 2,
                    AppVariableTypeId = 1002
                }
                );

            #endregion

            #region Fluent api for my Many-to-many implementation 

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAppVariableType>().HasKey(ua => new {ua.Id,  ua.UserId, ua.AppVariableTypeId });
            
            #endregion
        }
    }
}
