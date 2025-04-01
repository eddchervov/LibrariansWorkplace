using LibrariansWorkplace.Domain;

namespace LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;

public class DateBirthOutOfRangeException : Exception
{
    public DateBirthOutOfRangeException(DateTime dateBirth)
        : base($"Дата рождения вне диапозона, пришло {dateBirth.ToString("dd.MM.yyyy")}. Нужно от {Reader.MinDateBirth.ToString("dd.MM.yyyy")} до {Reader.MaxDateBirth.ToString("dd.MM.yyyy")}") { }
}
