using System;

namespace ShiftTrackingAPI.Helpers.Exceptions
{
    public enum ErrorType
    {
        /// <summary>нет ошибки</summary>
        None = 0,
        /// <summary>не найдено </summary>
        NotFound = 1,
        /// <summary>  введены неверные данные </summary>
        DataIncorrect = 2,
        /// <summary> работа с ошибками даты начала смены</summary>
        DateFromIncorrect = 3,
        /// <summary> работа с ошибками даты конца смены</summary>
        DateToIncorrect = 4,
        /// <summary> работа с ошибками даты</summary>
        DateIncorrect = 5,
    }
}
