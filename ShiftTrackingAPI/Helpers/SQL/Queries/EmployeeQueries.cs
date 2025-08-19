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
        public static async Task<EmployeeDTO> UpdateEmployee(AppDbContext context, EmployeeDTO obj, long id)
        {
            //можно было бы сделать sql и объединить методы апдейт и добавления сотрудника , предварительно проверив на существующий айдишник((
            var existingEmployee = await context.employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.LastName = obj.LastName;
            existingEmployee.FirstName = obj.FirstName;
            existingEmployee.MiddleName = obj.MiddleName;
            existingEmployee.Position = obj.Position;

            await context.SaveChangesAsync();
            return new EmployeeDTO
            {
                LastName = existingEmployee.LastName,
                FirstName = existingEmployee.FirstName,
                MiddleName = existingEmployee.MiddleName,
                Position = existingEmployee.Position
            };
        }
        public static async Task<string> DeleteEmployee(AppDbContext context, long id)
        {
            var existingEmployee = await context.employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return null;
            }
            context.employees.Remove(existingEmployee);
            await context.SaveChangesAsync();
            return "Сотрудник удалёне";
        }
    }
}
