namespace LibrariansWorkplace.Services.Interfaces.Common.Exceptions;

public class ThereAreNoAvailableCopiesOfLookException : Exception
{
    public ThereAreNoAvailableCopiesOfLookException(int bookId) : base($"Нет доступных копий книги с id={bookId}") { }
}