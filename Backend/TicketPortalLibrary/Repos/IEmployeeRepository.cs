using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface IEmployeeRepository 
{
<<<<<<< HEAD
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int empId);
    Task<Employee> GetEmployeeByIdAsync(int empId);
    Task<Employee> LoginEmployee(string email,string password);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);

=======
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(string empId);
    Task<Employee?> GetEmployeeByIdAsync(string empId);
    Task<Employee> LoginEmployee(string email,string password);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(string departmentId);
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
}