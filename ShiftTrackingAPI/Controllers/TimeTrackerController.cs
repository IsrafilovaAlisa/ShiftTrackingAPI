using Microsoft.AspNetCore.Mvc;
using ShiftTrackingAPI.Helpers.SQL;

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
    }
}
