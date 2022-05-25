using Identity.WebApi.Helpers;
using Identity.WebApi.Interfaces;
using Identity.WebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Identity.WebApi.Services
{
    public class LoginService : ILoginService
    {
        #region users
        //for simplicity the users are not mapped to the table
        private List<User> _users = new List<User>
        {
            new User { AccountNumber = 1, FullName = "Ruskin Bond", Username = "rusk", Password = "bond!w2q" },
            new User { AccountNumber = 2, FullName = "Arundhati Roy", Username = "arun", Password = "roy@123" },
             new User { AccountNumber = 4, FullName = "a", Username = "a", Password = "a" },
        };
        #endregion

        private readonly AppSettings _appSettings;

        public LoginService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Token Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("accountnumber", user.AccountNumber.ToString()),
                    new Claim("name", user.FullName)
                }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new Token() { auth_token = jwtSecurityToken };
        }
    }
}
