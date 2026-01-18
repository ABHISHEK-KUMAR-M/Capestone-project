using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketPortalLibrary.Repos;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task<Department> CreateDepartmentAsync(Department department)
    {
        try
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while creating department. " + ex.Message);
        }
    }

    public async Task<Department> UpdateDepartmentAsync(Department department)
    {
        var existing = await GetDepartmentByIdAsync(department.DepartmentId);

        try
        {
            existing.DepartmentName = department.DepartmentName;
            existing.Description = department.Description;

            await _context.SaveChangesAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while updating department. " + ex.Message);
        }
    }

    public async Task DeleteDepartmentAsync(int departmentId)
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .Include(d => d.TicketTypes)
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

        if (department == null)
        {
            throw new Exception("Department not found.");
        }
        else if (department.Employees.Any() || department.TicketTypes.Any())
        {
            throw new Exception("Department is in use. Remove related records before deleting.");
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }

    public async Task<Department?> GetDepartmentByIdAsync(int departmentId)
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .Include(d => d.TicketTypes)
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

        if (department == null)
        {
            throw new Exception("Department not found.");
        }

        return department;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        return await _context.Departments
            .Include(d => d.Employees)
            .Include(d => d.TicketTypes)
            .ToListAsync();
    }
}
