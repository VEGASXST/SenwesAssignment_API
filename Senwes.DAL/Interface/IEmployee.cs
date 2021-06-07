using Senwes.DAL.Model;
using System.Collections.Generic;

namespace Senwes.DAL.Interface
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAllEmployees();

        Employee GetByEmployeeId(int empId);

        IEnumerable<Employee> GetEmployeesOfTheLastFiveYears();

        IEnumerable<Employee> GetEmployeesOlderThen30();

        IEnumerable<Employee> GetTopHighestEarnings();

        IEnumerable<Employee> GetSearchEmployee(string param);

        List<int> GetAllEmployeesSalary(string firstName);

        List<string> GetAllCityNames();

        IEnumerable<Employee> AuthenticateEmployee(string userName, string password);
    }
}
