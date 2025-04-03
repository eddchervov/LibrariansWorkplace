namespace LibrariansWorkplace.Domain.Interfaces.Books;

public interface IBookRepository
{
    Task<Book?> Get(int id);
    Task<Book?> GetFull(int id);
    Task<IEnumerable<Book>> GetFull();
    Task<IEnumerable<Book>> SearchByName(string search);
    Task Add(Book book);
    Task Save();
}
