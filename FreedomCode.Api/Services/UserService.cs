using FreedomCode.Api.Contracts;
using FreedomCode.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreedomCode.Api.Services
{
    public class UserService : IUserService
    {
        public UserService(FreedomCodeContext context)
        {
            _context = context;
        }

        private readonly FreedomCodeContext _context;
            

        public User AuthenticateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList().Select(u => {
                u.Password = null; // returns a user without a password
                return u;
            });              
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null) user.Password = null; // returns user without a password
            return user;
        }
    }
}
