using Microsoft.EntityFrameworkCore;
using ShiftTrackingAPI.Helpers.Exceptions;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ShiftTrackingAPI.Helpers.SQL.Queries
{
    public static class StatisticQueries
    {
        public static async Task<ViolationEmployeeDTO?> GetEmployeeViolations(AppDbContext context, long id)
        {
            var employee = await context.employees
                .Include(e => e.shifts)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                throw new CustomException(ErrorType.NotFound, id); 
            }
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var violationCount = employee.shifts
        .Where(s => s.From.Month == currentMonth &&
                   s.From.Year == currentYear &&
                   s.To != null) // учитываем ТОЛЬКО закрытые смены
        .Count(s => s.IsViolation == true); //Если нарушение есть

            return new ViolationEmployeeDTO
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                Position = employee.Position,
                Violations = violationCount
            };
        }
    }
}
