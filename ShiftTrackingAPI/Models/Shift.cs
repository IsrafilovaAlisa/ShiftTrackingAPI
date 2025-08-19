using System;

namespace ShiftTrackingAPI.Models
{
    public class Shift
    {
        public long Id { get; set; }
        /// <summary>
        /// Время начало раблоты
        /// </summary>
        public DateTime From { get; set; }
        /// <summary>
        /// Время конца работы
        /// </summary>
        public DateTime? To { get; set; }
        /// <summary>
        /// отработанные часы 
        /// </summary>
        public double? WorkTime { get; set; }
        /// <summary>
        /// внешний ключ к сотрудникам
        /// </summary>
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
