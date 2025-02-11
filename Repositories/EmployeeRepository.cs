using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    // use repository design pattern
    public class EmployeeRepository : IEmployeeRepository
    {
        // create an access to the DbContext from repository
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) {  _context = context; }

        // add new employee
        public async Task AddEmployeeAsync(Employee employee)
        {
            // add employee to the database
            await _context.Employees.AddAsync(employee);
            // save changes
            await _context.SaveChangesAsync();
        }

        // get all employees
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            // return all employees
            return await _context.Employees.ToListAsync();
        }

        // get employee by ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            // return employee by ID
            return await _context.Employees.FindAsync(id);
        }

        // update employee
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            // check if employee exists
            _context.Employees.Update(employee);
            // save changes
            await _context.SaveChangesAsync();
        }

        // delete employee
        public async Task DeleteEmployeeAsync(int id)
        {
            // get employee by ID
            var employeeInDb = await _context.Employees.FindAsync(id);

            // check if employee exists
            if (employeeInDb == null)
            {
                throw new InvalidOperationException("Employee with Id: {id} not found");
            }

            // remove employee
            _context.Employees.Remove(employeeInDb);
            // save changes
            await _context.SaveChangesAsync();
        }
    }
}
