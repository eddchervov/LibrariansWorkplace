using LibrariansWorkplace.Domain;

namespace LibrariansWorkplace.Services.Interfaces.Books.Exceptions;

public class CountCopiesOutOfRangeException : Exception
{
    public CountCopiesOutOfRangeException(int countCopies) 
        : base($"Количество копий вне диапозона, пришло {countCopies}. Нужно от {Book.MinCountCopies} до {Book.MaxCountCopies}") { }
}
