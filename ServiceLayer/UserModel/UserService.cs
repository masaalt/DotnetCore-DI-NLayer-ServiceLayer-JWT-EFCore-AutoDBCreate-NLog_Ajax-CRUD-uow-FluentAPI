using DB.Core.Models;
using ServiceLayer.UnitOfWork;

namespace ServiceLayer.User
{
    public class UserService: IUserService
    {
        private IUnitOfWorkService _uow;
        public UserService(IUnitOfWorkService uow)
        {
            _uow = uow;
        }

        public bool CreateUser(Users data)
        {
            _uow.UserRepo.Insert(data);
            _uow.Commit();
            return true;
        }
    }
}
