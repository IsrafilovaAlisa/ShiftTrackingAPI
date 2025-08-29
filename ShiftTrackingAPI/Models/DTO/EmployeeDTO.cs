using System.ComponentModel.DataAnnotations;
#nullable enable
namespace ShiftTrackingAPI.Models.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Фамилия обязательна")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Имя обязательно")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Должность обязательна")]
        [EnumDataType(typeof(Position), ErrorMessage = "Несуществует указанной должности")]
        public Position Position { get; set; }
    }
}
