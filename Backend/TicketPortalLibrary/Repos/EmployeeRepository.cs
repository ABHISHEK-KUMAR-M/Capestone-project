using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketPortalLibrary.Repos;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        try
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while creating employee. " + ex.Message);
        }
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        var existing = await GetEmployeeByIdAsync(employee.EmpId);

        try
        {
            existing.Name = employee.Name;
            existing.Email = employee.Email;
            existing.Password = employee.Password;
            existing.Role = employee.Role;
            existing.DepartmentId = employee.DepartmentId;

            await _context.SaveChangesAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while updating employee. " + ex.Message);
        }
    }

    public async Task DeleteEmployeeAsync(int empId)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmpId == empId);

        if (employee == null)
        {
            throw new Exception("Employee not found.");
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int empId)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmpId == empId);

        if (employee == null)
        {
            throw new Exception("Employee not found.");
        }

        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
    {
        return await _context.Employees
            .Where(e => e.DepartmentId == departmentId)
            .Include(e => e.Department)
            .ToListAsync();
    }
}
