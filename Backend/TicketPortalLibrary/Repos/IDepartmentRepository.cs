using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface IDepartmentRepository
{
    Task CreateDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task DeleteDepartmentAsync(string departmentId);
    Task<Department?> GetDepartmentByIdAsync(string departmentId);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
}
