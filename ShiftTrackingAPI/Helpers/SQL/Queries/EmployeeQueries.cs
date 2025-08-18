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
    }
}
