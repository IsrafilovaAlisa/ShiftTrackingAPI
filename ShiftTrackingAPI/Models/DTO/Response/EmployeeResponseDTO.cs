namespace ShiftTrackingAPI.Models.DTO.Response
{
    public class EmployeeResponseDTO
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Position { get; set; }
    }
}
