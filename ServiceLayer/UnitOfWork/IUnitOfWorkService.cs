using DataLayer.Employee;
using DataLayer.User;

namespace ServiceLayer.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        //T GetRepository<T>() where T : class;
        IEmployeeRepo EmployeeRepo { get; }
        IUserRepo UserRepo { get; }
        int Commit();
    }
}
