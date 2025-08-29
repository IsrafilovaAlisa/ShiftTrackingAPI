using Microsoft.EntityFrameworkCore;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftTrackingAPI.Helpers.SQL.Queries
{
    public static class ShiftQueries
    {
        public static async Task<ShiftDTO> StartShift(AppDbContext context, long id, DateTime time)
        {
            var employee = await context.employees
                .Include(e => e.shifts)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                throw new CustomException(ErrorType.NotFound);
            }

            var activeShift = employee.shifts.FirstOrDefault(s => s.To == null);
            if (activeShift != null)
            {
                throw new CustomException(ErrorType.DateIncorrect);
            }

            var newShift = new Shift
            {
                EmployeeId = id,
                From = time,
                To = null,
                WorkTimeHours= null,
                IsViolation = false,
            };

            context.shifts.Add(newShift);
            await context.SaveChangesAsync();

            return new ShiftDTO
            {
                Id = newShift.Id,
                EmployeeId = newShift.EmployeeId,
                From = newShift.From,
                To = null,
                WorkTimeHours= null,
                IsViolation = false,
            };
        }
        public static async Task<ShiftDTO> EndShift(AppDbContext context, long id, DateTime time)
        {
            var employee = await context.employees
                .Include(e => e.shifts)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                throw new CustomException(ErrorType.NotFound);
            }

            var activeShift = employee.shifts.FirstOrDefault(s => s.To == null);

            if (activeShift == null)
            {
                throw new CustomException(ErrorType.DateIncorrect);
            }

            activeShift.To = time;
            activeShift.WorkTimeHours = (activeShift.To.Value - activeShift.From).TotalHours;
            activeShift.IsViolation = Violation.IsViolation(activeShift, employee.Position);
            if(activeShift.IsViolation == true)
            {
                throw new CustomException(ErrorType.DateIncorrect);
            }
            await context.SaveChangesAsync();

            return new ShiftDTO
            {
                Id = activeShift.Id,
                EmployeeId = id,
                From = activeShift.From,
                To = activeShift.To,
                WorkTimeHours = activeShift.WorkTimeHours,
                IsViolation = activeShift.IsViolation
            };
        }
    }
}
