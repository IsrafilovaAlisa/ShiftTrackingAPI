using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShiftTrackingAPI.Helpers.SQL;
using ShiftTrackingAPI.Helpers.SQL.Queries;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using ShiftTrackingAPI.Models.DTO.Response;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<EmployeeResponseDTO>> CreateEmployee([FromServices] AppDbContext context, [FromBody] EmployeeDTO obj)
        {
            return Ok(new { data = await EmployeeQueries.CreateEmployee(context, obj) });
        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromServices] AppDbContext context, [FromBody] EmployeeDTO obj, [FromQuery]long id)
        {
            var data = await EmployeeQueries.UpdateEmployee(context, obj, id);
            if (string.IsNullOrWhiteSpace(obj.LastName) || string.IsNullOrWhiteSpace(obj.FirstName) || !Enum.IsDefined(typeof(Position), obj.Position) || data == null)
            {
                return BadRequest(new { error = "Неверно введены данные" });
            }
            return Ok(new { data });
        }
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromServices] AppDbContext context, [FromQuery] long id)
        {
            var data = await EmployeeQueries.DeleteEmployee(context, id);
            if(data == null)
            {
                return BadRequest(new { error = "Сотрудника в списке нет" });
            }
            return Ok(new { data });
        }
    }
}
