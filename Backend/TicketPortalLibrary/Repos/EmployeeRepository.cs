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
            throw new TicketException("Unexpected error while creating employee. " + ex.Message,499);
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
            throw new TicketException("Unexpected error while updating employee. " + ex.Message,499);
        }
    }

    public async Task DeleteEmployeeAsync(int empId)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.AssignedTickets)
            .Include(e=>e.CreatedTickets)
            .FirstOrDefaultAsync(e => e.EmpId == empId);

        if (employee == null)
        {
            throw new TicketException("Employee not found.",404);
        }
        else if(employee.AssignedTickets.Count >0 || employee.CreatedTickets.Count > 0)
        {
            throw new TicketException("Employee is in use. Delete all the Employee Dependencies before deleting.",499);
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
            throw new TicketException("Employee not found.",404);
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
