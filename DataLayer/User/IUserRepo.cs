using DataLayer.Repository;
using DB.Core.Models;


namespace DataLayer.User
{
    public interface IUserRepo : IGenericRepository<Users>
    {
        UserDTO GetUser(Users userMode);
    }
}
