using System;

namespace ShiftTrackingAPI.Models.DTO
{
    public class ShiftDTO
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public double? WorkTime { get; set; }
    }
}
