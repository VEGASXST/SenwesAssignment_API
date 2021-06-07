using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Senwes.BAL.Service;
using Senwes.DAL.Interface;

namespace SenwesAssignment_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployee _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployee employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("Get/Authenticate")]
        [AllowAnonymous]
        private async Task<ActionResult<dynamic>> Authenticate(string userName = "swbuck", string password = "ja8?k3BTF^]o@<&;;")
        {
            var employee = _employeeService.AuthenticateEmployee(userName,password).FirstOrDefault();

            if (employee == null)
                return null;

            var token = TokenService.CreateToken(employee);
            return new
            {
                user = employee,
                token = token
            };
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        ///
        [HttpGet]
        [Route("Get/AllEmployees")]
        public IActionResult Get()
        {
            var authenticate = Authenticate();
            if (authenticate.Result == null) return NotFound(new {message = "User or password invalid"});
            var employeeData = _employeeService.GetAllEmployees();
            return Ok(employeeData);

        }

        /// <summary>
        /// Get Specific Employee
        /// </summary>
        /// <returns>Returns Specific Employee</returns>
        [HttpGet]
        [Route("Get/{empId}")]
        public IActionResult GetByEmployeeId(int empId)
        {
            var authenticate = Authenticate();
            if (authenticate.Result == null) return NotFound(new {message = "User or password invalid"});
            var employee = _employeeService.GetByEmployeeId(empId);
            return Ok(employee);
        }

        /// <summary>
        /// Get all employees that joined in the last 5 years
        /// </summary>
        /// <returns>Returns a list of employees that joined in the last 5 years </returns>
        [HttpGet]
        [Route("Get/GetEmployeesOfTheLastFiveYears")]
        public IActionResult GetEmployeesOfTheLastFiveYears()
        {
            var authenticate = Authenticate();
            if (authenticate.Result == null) return NotFound(new {message = "User or password invalid"});
            var filteredEmployees = _employeeService.GetEmployeesOfTheLastFiveYears();
            return Ok(filteredEmployees);
        }

        /// <summary>
        /// Get all employees order then 30 years of age
        /// </summary>
        /// <returns>Returns a list of all employees that are older then 30 years of age</returns>
        [HttpGet]
        [Route("Get/GetEmployeesOlderThen30")]
        public IActionResult GetEmployeesOlderThen30()
        {
            var authenticate = Authenticate();
            if (authenticate.Result == null) return NotFound(new {message = "User or password invalid"});
            var employee = _employeeService.GetEmployeesOlderThen30();
            return Ok(employee);
        }

        /// <summary>
        /// Get top 10 highest earnings of employees
        /// </summary>
        /// <returns>Returns a list of top 10 employees earnings</returns>
        [HttpGet]
        [Route("Get/GetTopHighestEarnings")]
        public IActionResult GetTopHighestEarnings()
        {
            var authenticate = Authenticate();
            if (authenticate.Result == null) return NotFound(new {message = "User or password invalid"});
            var topTenEarnings = _employeeService.GetTopHighestEarnings();
            return Ok(topTenEarnings);
        }

        /// <summary>
        /// Filter employee -Firstname, lastName, City
        /// </summary>
        /// <returns>Returns a list of employees based of filter</returns>
        [HttpGet]
        [Route("Get/SearchEmp/{param}")]
        public IActionResult GetSearchEmployee(string param)
        {
            var authenticate = Authenticate();
            if (authenticate.Result == null) return NotFound(new {message = "User or password invalid"});
            var filterEmployees = _employeeService.GetSearchEmployee(param);
            return Ok(filterEmployees);
        }

        /// <summary>
        /// Get all employees salary's that Firstname = "Treasure"
        /// </summary>
        /// <returns>Returns a list of salary's</returns>
        [HttpGet]
        [Route("Get/GetAllEmployeesSalary")]
        public IActionResult GetAllEmployeesSalary(string firstName = "Treasure")
        {
            var authenticate = Authenticate();
            if (authenticate.Result == null) return NotFound(new {message = "User or password invalid"});
            var filterEmployees = _employeeService.GetAllEmployeesSalary(firstName);
            return Ok(filterEmployees);
        }

        /// <summary>
        /// Get all City names
        /// </summary>
        /// <returns>Returns a list of all the city's</returns>
        [HttpGet]
        [Route("Get/GetAllCityNames")]
        [AllowAnonymous]
        public IActionResult GetAllCityNames()
        {
            //The Count in the query is for filtering out duplicate city's
            var filterCity = _employeeService.GetAllCityNames();
            return Ok(filterCity);
        }
    }
}
