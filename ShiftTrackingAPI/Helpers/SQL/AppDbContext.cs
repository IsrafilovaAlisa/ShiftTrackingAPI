using Microsoft.EntityFrameworkCore;
using ShiftTrackingAPI.Models;

namespace ShiftTrackingAPI.Helpers.SQL
{
    public class AppDbContext: DbContext
    {
        public DbSet<Employee> employees { get; set; }
        public DbSet<Shift> shifts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
