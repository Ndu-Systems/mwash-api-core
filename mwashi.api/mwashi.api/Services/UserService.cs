using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mwashi.api.Contracts;
using mwashi.api.Data;
using mwashi.api.Data.Entities;
using mwashi.api.Data.Models;
using mwashi.api.Helpers;

namespace mwashi.api.Services
{
    public class UserService : IUserService
    {
        public UserService(RepositoryDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        private readonly RepositoryDbContext _context;
        private readonly AppSettings _appSettings;
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList().Select(u => {
                u.Password = null; // returns a user without a password
                return u;
            });
        }

        public LoggedInUserModel AuthenticateUser(string username, string password)
        {
            var queryUser = _context.Users.Join(
                    _context.UserRoles,
                    u => u.UserId,
                    ur => ur.UserId,
                    (u, ur) => new {u, ur}
                )
                .Where(u => u.u.Email == username && u.u.Password == password).Take(1)
                .Join(
                    _context.Roles,
                    ur => ur.ur.RoleId,
                    r => r.RoleId,
                    (ur,r) => new LoggedInUserModel
                    {
                        UserId = ur.u.UserId,
                        Username = ur.u.Username,
                        Cellphone = ur.u.Cellphone,
                        Email = ur.u.Email,
                        CreateDate = ur.u.CreateDate,
                        Role = r.Name
                    }
                );

            var loggedInUser = new LoggedInUserModel();
            foreach (var u in queryUser)
            {
                loggedInUser = u;
            }

            if (loggedInUser == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loggedInUser.Email),
                    new Claim(ClaimTypes.Role, loggedInUser.Role),
                    new Claim(ClaimTypes.MobilePhone, loggedInUser.Cellphone),
                    
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            loggedInUser.Token = tokenHandler.WriteToken(token);

            return loggedInUser;
            
        }
    }
}
