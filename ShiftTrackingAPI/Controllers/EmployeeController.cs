using Microsoft.AspNetCore.Mvc;
using ShiftTrackingAPI.Helpers.SQL;
using ShiftTrackingAPI.Helpers.SQL.Queries;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using System;
using System.Collections.Generic;
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
        [HttpGet("Positions")]
        public ActionResult<IEnumerable<string>> GetPositions()
        {
            return Ok(Enum.GetNames(typeof(Position)));
        }
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromServices] AppDbContext context, [FromBody] EmployeeDTO obj)
        {
            if (string.IsNullOrWhiteSpace(obj.LastName) || string.IsNullOrWhiteSpace(obj.FirstName) || !Enum.IsDefined(typeof(Position), obj.Position))
            {
                return BadRequest(new { error = "Неверно введены данные" });
            }
            return Ok(new { data = await EmployeeQueries.CreateEmployee(context, obj) });
        }
        
    }
}
