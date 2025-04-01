namespace LibrariansWorkplace.Domain.Interfaces.Books;

public interface IBookRepository
{
    Task<Book?> Get(int id);
    Task<IEnumerable<Book>> Get();
    Task<IEnumerable<Book>> SearchByName(string search);
    Task Add(Book book);
    Task Save();
}
