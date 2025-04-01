namespace LibrariansWorkplace.Services.Interfaces.Common.Exceptions;

public class ReaderNotFoundException : Exception
{
    public ReaderNotFoundException(int readerId) : base($"Не найден читатель с id={readerId}") { }
}
