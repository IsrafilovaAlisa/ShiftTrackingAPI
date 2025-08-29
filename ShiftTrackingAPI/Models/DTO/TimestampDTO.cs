using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftTrackingAPI.Models.DTO
{
    public class TimestampDTO
    {
        [Required(ErrorMessage = "Укажите время")]
        public DateTime Timestamp { get; set; }
    }
}
