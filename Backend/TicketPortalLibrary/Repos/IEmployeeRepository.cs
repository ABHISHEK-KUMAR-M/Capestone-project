using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface IEmployeeRepository 
{
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(string empId);
    Task<Employee?> GetEmployeeByIdAsync(string empId);
    Task<Employee> LoginEmployee(string email,string password);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(string departmentId);
}