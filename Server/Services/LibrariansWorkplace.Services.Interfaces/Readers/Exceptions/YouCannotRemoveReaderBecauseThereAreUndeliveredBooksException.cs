namespace LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;

public class YouCannotRemoveReaderBecauseThereAreUndeliveredBooksException : Exception
{
    public YouCannotRemoveReaderBecauseThereAreUndeliveredBooksException(int readerId) 
        : base($"Нельзя удалить читателя с id={readerId}, так как есть не сданные книги") { }
}
