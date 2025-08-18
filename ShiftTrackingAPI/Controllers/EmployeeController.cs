using Microsoft.AspNetCore.Mvc;
using ShiftTrackingAPI.Helpers.SQL;
using ShiftTrackingAPI.Helpers.SQL.Queries;
using ShiftTrackingAPI.Models;
using System;
using System.Threading.Tasks;
#nullable enable
namespace ShiftTrackingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController: ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee([FromServices] AppDbContext context,  [FromQuery]Position? position)
        {
            if(position != null)
            {
                if (!Enum.IsDefined(typeof(Position), position))
                {
                    return BadRequest(new { error = "Нет такой должности" });
                }
            }
            var result = await EmployeeQueries.GetEmployee(context, position);
            return Ok(new { data = result });
        }
    }
}
