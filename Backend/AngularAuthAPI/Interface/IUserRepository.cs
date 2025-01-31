using AngularAuthAPI.Models;

namespace AngularAuthAPI.Interface
{
    public interface IUserRepository
    {
        public Task<User> Authenticate(string UserName, string Password);
        void Register(string fname,string lname,string email,string name, string password);
        Task<bool> UserAlreadyRegistered(string UserName,string email);
    }
}
