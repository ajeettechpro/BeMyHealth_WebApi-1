using BeMyHealth_WebApi.Models;

namespace BeMyHealth_WebApi.Services.UserService
{
    public interface IUserService
    {
        Task<bool> CreateRegister(User user);
        Task<string> Login(string emailIdOrmobileNumber, string password);

    }
}
