namespace LibrariansWorkplace.Services.Interfaces.Common.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(int bookId) : base($"Не найдена книга с id={bookId}") { }
}
