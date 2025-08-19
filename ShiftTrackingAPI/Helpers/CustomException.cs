using System;

namespace ShiftTrackingAPI.Helpers
{
    public class CustomException: Exception
    {
        public ErrorType Type { get; }
        public CustomException(ErrorType type) : base(type.ToString()) { Type = type; }
    }
}
