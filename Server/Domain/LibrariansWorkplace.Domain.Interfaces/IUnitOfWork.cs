using LibrariansWorkplace.Domain.Interfaces.Books;

namespace LibrariansWorkplace.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository BookRepository { get; }
    IReaderRepository ReaderRepository { get; }

    void Commit();
    void Rollback();
}
