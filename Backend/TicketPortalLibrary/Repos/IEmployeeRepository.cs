using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface IEmployeeRepository 
{
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(string empId,Employee employee);
    Task DeleteEmployeeAsync(string empId);
    Task<Employee?> GetEmployeeByIdAsync(string empId);
    Task<Employee> LoginEmployee(string empId,string password);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(string departmentId);
}