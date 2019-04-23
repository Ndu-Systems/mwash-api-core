using FreedomCode.Api.Contracts;
using FreedomCode.Api.Data;
using FreedomCode.Api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FreedomCode.Api.Services
{
    public class UserService : IUserService
    {
        public UserService(FreedomCodeContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        private readonly FreedomCodeContext _context;
        private readonly AppSettings _appSettings;

        public User AuthenticateUser(string username, string password)
        {
            //var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            var queryuser = _context.Users.Join(
                _context.UserAppVariableType,
                userA => userA.UserId,
                userAppA => userAppA.UserId,
                (userA, userAppA) => new
                {
                    User = userA,
                    userAppA
                })
                .Join(
                    _context.AppVariableType,
                    userAppVar => userAppVar.userAppA.AppVariableTypeId,
                    appVarA => appVarA.AppVariableTypeId,
                    (userAppVar,appVarA) => new
                    {
                        userAppVar.User,
                        appVarA.Name
                    }
                )
                .Where(u => u.User.Username == username && u.User.Password == password)
                .Take(1);

            User user = null;
            string role = null;

            foreach (var u in queryuser)
            {
                user = u.User;
                role = u.Name;
            };

            // return null if user not found.
            if (user == null) return null;

            // authentication success issue jwt token;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
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
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null) user.Password = null; // returns user without a password
            return user;
        }
    }
}
