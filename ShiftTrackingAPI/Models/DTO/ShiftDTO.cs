using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftTrackingAPI.Models.DTO
{
    public class ShiftDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Сотрудника нет")]
        public long EmployeeId { get; set; }
        [Required(ErrorMessage = "Укажите время начала смены")]
        public DateTime? From { get; set; }
        [Required(ErrorMessage = "Укажите время конца смены")]
        public DateTime? To { get; set; }
        public double? WorkTimeHours { get; set; }
        public bool? IsViolation { get; set; }
    }
}
