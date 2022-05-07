using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationEmp.Model;
using WebApplicationEmp.Service;

namespace WebApplicationEmp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            return _employeeService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public IActionResult Get(string id)
        {
            var emp = _employeeService.Get(id);

            if (emp == null)
            {
                return NotFound("No match found");
            }

            return Ok(emp);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddEmployee(Employee emp)
        {
            try
            {
                 _employeeService.AddEmployee(emp);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteEmployee(string id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
