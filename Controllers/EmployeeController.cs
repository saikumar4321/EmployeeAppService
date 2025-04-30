using EmployeeApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeApp.Model.Request;
using EmployeeApp.DAL.IRepositories;
using Newtonsoft.Json;


namespace EmployeeAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployee _employeeController;

        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployee employee, ILogger<EmployeeController> logger)
        {

            _employeeController = employee;
            _logger = logger;
        }


        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] EmployeeData employee)
        {
            _logger.LogInformation("AddEmployee" + JsonConvert.SerializeObject(employee));
            if (employee == null)
            {
                return BadRequest("Employee not Available");
            }
            _employeeController.AddEmployee(employee);

            return Ok(employee);

            //return CreatedAtRoute("Get",

            //    new {id=  customer.Id},customer);
        }


        [HttpGet("GetEmployee")]

        public IActionResult GetEmployee()
        {
            IEnumerable<EmployeeData> customers = _employeeController.GetEmployee();

            return Ok(customers);
        }


        [HttpGet("GetEmployeeByID")]
        public IActionResult GetEmployeeByID(int EMPId)
        {
            EmployeeData employee = _employeeController.GetEmployeeById(EMPId);
            if (employee == null)
            {
                return NotFound("Customer Can't Found");
            }
            return Ok(employee);
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(int EMPId, [FromBody] EmployeeData employee)
        {
            if (employee == null)
            {
                return BadRequest("Customer Not Available");

            }
            EmployeeData employeetoupdate = _employeeController.GetEmployeeById(EMPId);
            if (employeetoupdate == null)
            {
                return BadRequest("Customer Not Updated");
            }
            _employeeController.UpdateEmployee(employeetoupdate, employee);
            return Ok(employee);

        }

        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int EMPId)
        {
            EmployeeData employee = _employeeController.GetEmployeeById(EMPId);
            if (employee == null)
            {
                return NotFound("Data Not Found");
            }
            _employeeController.DeleteEmployee(employee);
            return Ok(employee);

        }

    }
}
