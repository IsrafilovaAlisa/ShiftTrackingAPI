namespace ShiftTrackingAPI.Models.DTO
#nullable enable
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public Position? Position { get; set; }
    }
}
