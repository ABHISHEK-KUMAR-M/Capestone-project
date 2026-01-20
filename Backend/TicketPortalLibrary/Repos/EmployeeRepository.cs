using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace TicketPortalLibrary.Repos;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task CreateEmployeeAsync(Employee employee)
    {
        try
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while updating employee. {ex.Message}" ,499);
        }
    }

    public async Task UpdateEmployeeAsync(Employee employee)
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
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while updating employee. {ex.Message}" ,499);
        }
    }

    public async Task DeleteEmployeeAsync(string empId)
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
        try{    
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
    }

    public async Task<Employee?> GetEmployeeByIdAsync(string empId)
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
        var Employee=await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();

        return Employee;
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(string departmentId)
    {
        var Employee=await _context.Employees
            .Where(e => e.DepartmentId == departmentId)
            .Include(e => e.Department)
            .ToListAsync();
        if (Employee.Count == 0)
        {
            throw new TicketException("Employee not found.",404);
        }
        return Employee;
    }

    public async Task<Employee> LoginEmployee(string email, string password)
    {
        var Employee=await _context.Employees.FirstOrDefaultAsync(e=>e.Email==email && e.Password==password);
        if (Employee == null)
        {
            throw new TicketException("Employee not found.",404);
        }
        return Employee;
    }
}

