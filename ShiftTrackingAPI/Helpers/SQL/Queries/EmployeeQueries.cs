using Microsoft.EntityFrameworkCore;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftTrackingAPI.Helpers.SQL.Queries
{
    public static class EmployeeQueries
    {
        public static async Task<List<EmployeeDTO>> GetEmployee(AppDbContext context, Position? position)
        {
            var query = context.employees.AsNoTracking();
            return await query
                .Where(e => position == null || e.Position == position)
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    Position = e.Position
                }).ToListAsync();
        } 
        public static async Task<EmployeeDTO> CreateEmployee(AppDbContext context, EmployeeDTO obj)
        {
            var newEmployee = new Employee
            {
                LastName = obj.LastName,
                FirstName = obj.FirstName,
                MiddleName = obj.MiddleName,
                Position = obj.Position
            };
            context.employees.Add(newEmployee);
            await context.SaveChangesAsync();
            return new EmployeeDTO
            {
                Id = newEmployee.Id,
                LastName = newEmployee.LastName,
                FirstName = newEmployee.FirstName,
                MiddleName = newEmployee.MiddleName,
                Position= newEmployee.Position
            };

        }
    }
}
