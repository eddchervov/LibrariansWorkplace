namespace LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;

public class BookNotFoundForReaderException : Exception
{
    public BookNotFoundForReaderException(int bookId) : base($"Нет доступных копий книги с id={bookId}") { }
}
