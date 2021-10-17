using DataLayer.Employee;
using DataLayer.User;
using DB.Core.Models;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private IEmployeeRepo employeeRepo { get; set; }
        private IUserRepo userRepo { get; set; }



        public MVCEFCoreContext _dbContext;
        private IConfiguration _config;
        public UnitOfWorkService(MVCEFCoreContext context, IConfiguration iConfig)
        {
            _dbContext = context;
            _config = iConfig;
        }
        public IEmployeeRepo EmployeeRepo => employeeRepo == null ? new EmployeeRepo(_dbContext) : employeeRepo;
        public IUserRepo UserRepo => userRepo == null ? new UserRepo(_dbContext, _config) : userRepo;

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}
