using System;

namespace ShiftTrackingAPI.Helpers
{
    public enum ErrorType
    {
        /// <summary>нет ошибки</summary>
        None = 0,
        /// <summary>не найдено </summary>
        NotFound = 1,
        /// <summary>  введены неверные данные </summary>
        DataIncorrect = 2,
        /// <summary> работа с ошибками даты</summary>
        DateIncorrect = 3,
    }
}
