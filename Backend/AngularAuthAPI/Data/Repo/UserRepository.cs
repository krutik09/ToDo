using AngularAuthAPI.Interface;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Cryptography;
using System.Text;

namespace AngularAuthAPI.Data.Repo
{
    public class UserRepository:IUserRepository
    {
        private DBC dbc;

        public UserRepository(DBC dbc)
        {
            this.dbc = dbc;
        }

        public async Task<User> Authenticate(string UserName, string Password)
        {
            var user = await dbc.Users.FirstOrDefaultAsync(x => x.Username == UserName);
            if (user==null)
            {
                return null;
            }
            using (var hmac= new HMACSHA512(user.PasswordKey))
            {
                var PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < PasswordHash.Length; i++)
                {
                    if (PasswordHash[i] != user.PasswordHash[i])
                    {
                        return null;
                    }
                }
                return user;
            }
        }

        public void Register(string fname,string lname,string email,string name, string password)
        {
            byte[] PasswordHash, PasswordKey;
            using (var hmac=new HMACSHA512())
            {
                PasswordKey = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            User user=new User();
            user.Username = name;
            user.PasswordKey = PasswordKey;
            user.PasswordHash = PasswordHash;
            user.Email = email;
            user.FirstName = fname;
            user.LastName=lname;
            dbc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyRegistered(string UserName,string email)
        {
            return await dbc.Users.AnyAsync(x => x.Username == UserName||x.Email==email);
        }
    }
}
