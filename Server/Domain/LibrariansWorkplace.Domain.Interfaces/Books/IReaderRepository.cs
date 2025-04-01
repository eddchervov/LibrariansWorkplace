namespace LibrariansWorkplace.Domain.Interfaces.Books;

public interface IReaderRepository
{
    Task<Reader?> Get(int id);
    Task<IEnumerable<Reader>> SearchByFullName(string search);
    Task Add(Reader book);
    Task Save();
}