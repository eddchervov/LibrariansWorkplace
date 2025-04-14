using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Helpers;

namespace LibrariansWorkplace.Services.Interfaces.Books.Exceptions;

public class CountCopiesOutOfRangeException : Exception
{
    public CountCopiesOutOfRangeException(int countCopies) 
        : base($"Количество копий вне диапозона, пришло {countCopies}. Нужно от {BookSetup.MinCountCopies} до {BookSetup.MaxCountCopies}") { }
}
