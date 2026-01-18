using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface IDepartmentRepository
{
    Task<Department> CreateDepartmentAsync(Department department);
    Task<Department> UpdateDepartmentAsync(Department department);
    Task DeleteDepartmentAsync(int departmentId);
    Task<Department?> GetDepartmentByIdAsync(int departmentId);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
}
