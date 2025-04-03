namespace LibrariansWorkplace.Services.Interfaces.Books.Exceptions;

public class YouCanNotDeleteBookBecauseThereAreCopiesIssuedException : Exception
{
    public YouCanNotDeleteBookBecauseThereAreCopiesIssuedException(int bookdId) 
        : base($"Нельзя удалить книгу с id={bookdId}, так как у нее есть выданные читателям экземпляры") { }
}
