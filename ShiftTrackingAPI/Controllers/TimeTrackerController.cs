using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShiftTrackingAPI.Helpers.SQL;
using ShiftTrackingAPI.Helpers.SQL.Queries;
using ShiftTrackingAPI.Models.DTO;
using ShiftTrackingAPI.Models.DTO.Response;
using System.Threading.Tasks;

namespace ShiftTrackingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeTrackerController: ControllerBase
    {
        private readonly AppDbContext _context;
        public TimeTrackerController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpPost("StartShift")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShiftDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ShiftDTO> StartShift([FromServices]AppDbContext context, long id, TimestampDTO time)
        {
            return await ShiftQueries.StartShift(context, id, time.Timestamp);
        }
        [HttpPost("EndShift")]
        public async Task<ShiftDTO> EndFinish([FromServices]AppDbContext context, long id, TimestampDTO time)
        {
            return await ShiftQueries.EndShift(context, id, time.Timestamp);
        }
        [HttpGet("GetStatisticViolation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShiftDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ViolationEmployeeDTO> GetStatisticViolation([FromServices] AppDbContext context, [FromQuery] long id)
        {
            return await StatisticQueries.GetEmployeeViolations(context, id);
            
        }
    }
}
