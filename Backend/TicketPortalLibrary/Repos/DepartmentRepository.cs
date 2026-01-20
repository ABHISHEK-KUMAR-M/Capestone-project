using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace TicketPortalLibrary.Repos;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task CreateDepartmentAsync(Department department)
    {
        try
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while creating department. {ex.Message}" ,499);
        }
    }
    public async Task UpdateDepartmentAsync(string departmentId,Department department)
    {
        var existing = await GetDepartmentByIdAsync(departmentId);

        try
        {
            existing.DepartmentName = department.DepartmentName;
            existing.Description = department.Description;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while updating department. {ex.Message}" ,499);
        }
    }

    public async Task DeleteDepartmentAsync(string departmentId)
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .Include(d => d.TicketTypes)
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

        if (department == null)
        {
            throw new TicketException("Department not found.",404);
        }
        else if (department.Employees.Any() || department.TicketTypes.Any())
        {
            throw new TicketException("Department is in use. Remove related records before deleting.",499);
        }

        try{    
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
    }

    public async Task<Department?> GetDepartmentByIdAsync(string departmentId)
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .Include(d => d.TicketTypes)
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

        if (department == null)
        {
            throw new TicketException("Department not found.",404);
        }

        return department;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .Include(d => d.TicketTypes)
            .ToListAsync();

        return department;

    }
}
