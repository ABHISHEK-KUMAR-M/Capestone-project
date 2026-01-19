using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface IEmployeeRepository 
{
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int empId);
    Task<Employee> GetEmployeeByIdAsync(int empId);
    Task<Employee> LoginEmployee(string email,string password);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);

}