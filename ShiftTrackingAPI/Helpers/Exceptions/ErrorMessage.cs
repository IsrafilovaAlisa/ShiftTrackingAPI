namespace ShiftTrackingAPI.Helpers.Exceptions
{
    public static class ErrorMessage
    {
        public static string GetMessage(ErrorType type, params object[] args)
        {
            var template = type switch
            {
                ErrorType.NotFound => "Сотрудник с идентификатором {0} не найден",
                ErrorType.DataIncorrect => "Введены неверные данные",
                ErrorType.DateFromIncorrect => "У сотрудника не введен конец смены",
                ErrorType.DateToIncorrect => "У сотрудника не введено начало смены",
                ErrorType.DateIncorrect => "Некорректно введена дата",
                _ => "дефолт ошибка"
            };
            return string.Format(template, args);
        }
    }
}
