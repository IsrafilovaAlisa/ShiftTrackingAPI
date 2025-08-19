using Microsoft.AspNetCore.Mvc;
using ShiftTrackingAPI.Helpers;
using ShiftTrackingAPI.Helpers.SQL;
using ShiftTrackingAPI.Helpers.SQL.Queries;
using ShiftTrackingAPI.Models;
using ShiftTrackingAPI.Models.DTO;
using System;
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
        public async Task<IActionResult> StartShift([FromServices]AppDbContext context, long id, TimestampDTO time)
        {
            try
            {
                var data = await ShiftQueries.StartShift(context, id, time.Timestamp);
                return Ok(data);
            }
            catch (CustomException ex) {
                switch (ex.Type) 
                {
                    case ErrorType.NotFound: return BadRequest(new { error = "Неверно введен номер сотрудника" });
                    case ErrorType.DateIncorrect: return BadRequest(new { error = "У сотрудника не введен конец смена" });
                    default: return BadRequest();
                }
            }
        }
        [HttpPost("EndShift")]
        public async Task<IActionResult> EndFinish([FromServices]AppDbContext context, long id, TimestampDTO time)
        {
            try
            {
                var data = await ShiftQueries.EndShift(context, id, time.Timestamp);
                return Ok(data);
            }
            catch (CustomException ex)
            {
                switch (ex.Type)
                {
                    case ErrorType.NotFound: return BadRequest(new { error = "Неверно введен номер сотрудника" });
                    case ErrorType.DateIncorrect: return BadRequest(new { error = "У сотрудника не введено начало смены" });
                    default: return BadRequest();
                }
            }
        }
        [HttpGet("GetStatisticViolation")]
        public async Task<IActionResult> GetStatisticViolation([FromServices] AppDbContext context, [FromQuery] Employee obj)
        {
            return Ok(new { data = StatisticQueries.CalculateViolationsThisMonth(context, obj) });
        }
    }
}
