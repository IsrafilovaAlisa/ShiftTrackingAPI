using ShiftTrackingAPI.Models;
using System;

namespace ShiftTrackingAPI.Helpers
{
    public static class Violation
    {
        public static bool IsViolation(Shift shift, Position position)
        {
            if (shift.To == null) return false;

            if (position != Position.Tester)
            {
                return shift.From.TimeOfDay > TimeSpan.FromHours(9) ||
                       shift.To.Value.TimeOfDay < TimeSpan.FromHours(18);
            }

            return shift.To.Value.TimeOfDay < TimeSpan.FromHours(21);
        }
    }
}
