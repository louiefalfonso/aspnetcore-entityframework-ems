using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    // create repository interface
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>>GetAllAsync();
        Task<Employee?>GetByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
