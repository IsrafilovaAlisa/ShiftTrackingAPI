using Microsoft.AspNetCore.Authentication;

namespace ShiftTrackingAPI.Models.DTO
{
    public class ViolationEmployeeDTO
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public Position Position { get; set; }
        public int Violations { get; set; } 
    }
}
