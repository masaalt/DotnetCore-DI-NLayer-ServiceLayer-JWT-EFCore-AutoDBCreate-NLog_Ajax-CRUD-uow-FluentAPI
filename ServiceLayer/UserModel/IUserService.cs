using DB.Core.Models;

namespace ServiceLayer.User
{
    public interface IUserService
    {
        bool CreateUser(Users data);
    }
}
