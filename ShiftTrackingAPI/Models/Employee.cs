using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;

namespace ShiftTrackingAPI.Models
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    public class Employee
    { 
        /// <summary>
        /// Номер пропуска
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество (если есть)
        /// </summary>
        public string? MiddleName { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public Position Position { get; set; }
        /// <summary>
        /// список Смен
        /// </summary>
        public List<Shift> shifts { get; set; } = new List<Shift>();

    }

}
