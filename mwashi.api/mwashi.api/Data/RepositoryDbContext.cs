using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mwashi.api.Data.Entities;

namespace mwashi.api.Data
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
