using ShiftTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiftTrackingAPI.Helpers.SQL
{
    public static class DataGenerator
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.employees.Any())
            {
                var employees = new List<Employee>
            {
                new() { FirstName = "Вовочка", LastName = "Свечка", Position = Position.Tester},
                new() { FirstName = "Мария", LastName = "Хорошегодня", Position = Position.Engineer },
                new() { FirstName = "Алексей", LastName = "мывамперезвоним", Position = Position.Manager},
                new() { FirstName = "test", LastName = "test", Position = Position.Tester },
                new() { FirstName = "test", LastName = "test", Position = Position.Tester }
            };

                context.employees.AddRange(employees);
                context.SaveChanges();

                // Генерация тестовых смен с нарушениями
                var random = new Random();
                var now = DateTime.Now;

                foreach (var employee in employees)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        var startDate = now.AddDays(-random.Next(30));
                        var startTime = employee.Position == Position.Tester
                            ? startDate.Date.AddHours(8 + random.Next(3)) // 8-11 для тестировщиков
                            : startDate.Date.AddHours(7 + random.Next(4)); // 7-11 для остальных

                        var endTime = employee.Position == Position.Tester
                            ? startTime.AddHours(10 + random.Next(4)) // 10-14 часов для тестировщиков
                            : startTime.AddHours(8 + random.Next(3)); // 8-11 часов для остальных

                        var shift = new Shift
                        {
                            EmployeeId = employee.Id,
                            From = startTime,
                            To = endTime,
                            WorkTimeHours = (endTime - startTime).TotalHours
                        };
                        shift.IsViolation = Violation.IsViolation(shift, employee.Position);

                        context.shifts.Add(shift);

                    }
                }

                context.SaveChanges();
            }
        }
    }
}
