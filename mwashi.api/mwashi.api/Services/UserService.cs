using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mwashi.api.Contracts;
using mwashi.api.Data;
using mwashi.api.Data.Entities;

namespace mwashi.api.Services
{
    public class UserService : IUserService
    {
        public UserService(RepositoryDbContext context)
        {
            _context = context;
        }
        private readonly RepositoryDbContext _context;
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList().Select(u => {
                u.Password = null; // returns a user without a password
                return u;
            });
        }
    }
}
