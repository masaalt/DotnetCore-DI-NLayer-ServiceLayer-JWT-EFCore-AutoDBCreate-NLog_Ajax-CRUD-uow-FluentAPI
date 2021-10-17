using DB.Core.Models;

namespace ServiceLayer.Employee
{
    public interface IEmployeeService
    {
        bool CreateEmployee(Employees data);
    }
}
