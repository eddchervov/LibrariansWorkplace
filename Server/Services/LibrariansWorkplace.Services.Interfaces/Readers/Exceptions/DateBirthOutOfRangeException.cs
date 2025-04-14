using LibrariansWorkplace.Domain.Helpers;

namespace LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;

public class DateBirthOutOfRangeException : Exception
{
    public DateBirthOutOfRangeException(DateTime dateBirth)
        : base($"Дата рождения вне диапозона, пришло {dateBirth:dd.MM.yyyy}. Нужно от {ReaderSetup.MinDateBirth:dd.MM.yyyy} до {ReaderSetup.MaxDateBirth:dd.MM.yyyy}") { }
}
