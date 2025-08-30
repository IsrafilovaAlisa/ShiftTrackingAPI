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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        public async Task<List<ViolationEmployeeDTO>> GetEmployee([FromServices] AppDbContext context,  [FromQuery]Position? position)
        {
            return await EmployeeQueries.GetEmployee(context, position);
        }
        [HttpGet("Positions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        public IEnumerable<string> GetPositions()
        {
            return (Enum.GetNames(typeof(Position)));
        }

        [HttpPost("CreateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<EmployeeDTO> CreateEmployee([FromServices] AppDbContext context, [FromBody] EmployeeDTO obj)
        {
            return await EmployeeQueries.CreateEmployee(context, obj);
        }

        [HttpPut("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<EmployeeDTO> UpdateEmployee([FromServices] AppDbContext context, [FromBody] EmployeeDTO obj, [FromQuery]long id)
        {
            return await EmployeeQueries.UpdateEmployee(context, obj, id);
        }
        [HttpDelete("DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<string> DeleteEmployee([FromServices] AppDbContext context, [FromQuery] long id)
        {
            return await EmployeeQueries.DeleteEmployee(context, id);
        }
    }
}
