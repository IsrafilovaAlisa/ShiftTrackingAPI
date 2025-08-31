using Microsoft.EntityFrameworkCore;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using System;
using System.Linq;
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
                return null;
            }
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var monthlyShifts = employee.shifts
                .Where(s => s.From.Month == currentMonth && s.From.Year == currentYear)
                .ToList();

            var violations = monthlyShifts.Count(shift => Violation.IsViolation(shift, employee.Position));

            return new ViolationEmployeeDTO
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                Position = employee.Position,
                Violations = violations
            };
        }
    }
}
