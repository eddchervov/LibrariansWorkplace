using LibrariansWorkplace.Domain;

namespace LibrariansWorkplace.Services.Interfaces.Books.Exceptions;

public class YearPublicationOutOfRangeException : Exception
{
    public YearPublicationOutOfRangeException(int yearPublication, int currentYear)
        : base($"Год публикации вне диапозона, пришло {yearPublication}. Нужно от {Book.MinYearPublication} до {currentYear}") { }
}

