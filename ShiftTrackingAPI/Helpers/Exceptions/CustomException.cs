using System;

namespace ShiftTrackingAPI.Helpers.Exceptions
{
    public class CustomException : Exception
    {
        public ErrorType Type { get; }
        public CustomException(ErrorType type, params object[] args) : base(ErrorMessage.GetMessage(type, args)) 
        {
            Type = type; 
        }
    }
}
