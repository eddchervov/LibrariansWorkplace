namespace LibrariansWorkplace.Domain.Interfaces.Books;

public interface IReaderRepository
{
    Task<Reader?> Get(int id);
    Task<Reader?> GetFull(int id);
    Task<IEnumerable<Reader>> GetAll();
    Task<IEnumerable<Reader>> SearchByFullName(string search);
    Task Add(Reader book);
    Task Save();
}