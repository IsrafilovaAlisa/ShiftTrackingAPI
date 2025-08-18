using Microsoft.AspNetCore.Mvc;
using ShiftTrackingAPI.Helpers.SQL;

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
    }
}
