using LibrariansWorkplace.Domain.Helpers;

namespace LibrariansWorkplace.Services.Interfaces.Books.Exceptions;

public class YearPublicationOutOfRangeException : Exception
{
    public YearPublicationOutOfRangeException(int yearPublication, int currentYear)
        : base($"Год публикации вне диапозона, пришло {yearPublication}. Нужно от {BookSetup.MinYearPublication} до {currentYear}") { }
}

