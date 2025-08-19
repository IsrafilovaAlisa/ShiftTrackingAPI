using Microsoft.EntityFrameworkCore;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using System;
using System.Collections.Generic;
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
                 WorkTime= null
            };

            context.shifts.Add(newShift);
            await context.SaveChangesAsync();

            return new ShiftDTO
            {
                Id = newShift.Id,
                EmployeeId = newShift.EmployeeId,
                From = newShift.From,
                To = null
            };
        }
       
    }
}
