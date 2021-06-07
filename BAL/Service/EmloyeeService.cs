using Senwes.DAL.Data;
using Senwes.DAL.Interface;
using Senwes.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Senwes.BAL.Service
{
    public class EmloyeeService : IEmployee
    {
        private readonly LoadData _loadData;

        public EmloyeeService()
        {
            _loadData = new LoadData();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _loadData.LoadEmployeeData().ToList();
        }

        public Employee GetByEmployeeId(int empId)
        {
            return _loadData.LoadEmployeeData().FirstOrDefault(x => x.EmpID == empId);
        }

        public IEnumerable<Employee> GetEmployeesOfTheLastFiveYears()
        {
            var currentDateNowYear = DateTime.Now.Year - 5;
            return (from item in _loadData.LoadEmployeeData() let convertedDate = (DateTime.Parse(item.DateOfJoining)).Year >= currentDateNowYear where convertedDate select item).ToList();
        }

        public IEnumerable<Employee> GetEmployeesOlderThen30()
        {
            return _loadData.LoadEmployeeData().Where(x => x.Age >= 30).ToList();
        }

        public IEnumerable<Employee> GetTopHighestEarnings()
        {
            return _loadData.LoadEmployeeData().OrderByDescending(x => x.Salary).Take(10).ToList();
        }

        public IEnumerable<Employee> GetSearchEmployee(string param)
        {
            return _loadData.LoadEmployeeData().Where(x=>x.FirstName == param || x.LastName == param || x.City == param).ToList();
        }

        public List<int> GetAllEmployeesSalary(string firstName)
        {
            return _loadData.LoadEmployeeData().Where(x=>x.FirstName == firstName).Select(x=>x.Salary).ToList();
        }

        public List<string> GetAllCityNames()
        {
            return _loadData.LoadEmployeeData().GroupBy(x=>x.City).Where(z=>z.Count() > 1).Select(x=>x.Key).ToList();
        }

        public IEnumerable<Employee> AuthenticateEmployee(string userName, string password)
        {
            return _loadData.LoadEmployeeData().Where(x => x.UserName == userName && x.Password == password);
        }
    }
}
