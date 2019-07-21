using System.Collections.Generic;
using mwashi.api.Data.Entities;
using mwashi.api.Data.Models;

namespace mwashi.api.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        LoggedInUserModel AuthenticateUser(string username, string password);
    }
}
