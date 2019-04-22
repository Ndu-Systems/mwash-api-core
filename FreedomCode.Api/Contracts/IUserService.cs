using FreedomCode.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreedomCode.Api.Contracts
{
    public interface IUserService
    {
        User AuthenticateUser(string username, string password);
        IEnumerable<User> GetAllUsers();

        User GetUserById(int id);
    }
}
