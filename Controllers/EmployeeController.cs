using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository) {

            _employeeRepository = employeeRepository;
        }

        // get all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployessAsync()
        {
            var allEmployess = await _employeeRepository.GetAllAsync();
            return Ok(allEmployess);
        }

        // get employee by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) { 
                return NotFound();
            }
            return Ok(employee);
        }

        // add new employee
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateNewEmployee(Employee employee)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            await _employeeRepository.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new {id = employee.Id }, employee);
        }

        // update employee
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UppdateEmployeeAsync(int id, Employee employee) 
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid == false)
            {
                return BadRequest();
            }

            await _employeeRepository.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // delete employee
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeById(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
