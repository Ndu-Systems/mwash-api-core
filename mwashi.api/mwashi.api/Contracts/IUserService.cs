using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mwashi.api.Data.Entities;

namespace mwashi.api.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
    }
}
