using LibrariansWorkplace.Domain.Interfaces.Books;
using LibrariansWorkplace.Domain.Interfaces.IssuedBooks;
using LibrariansWorkplace.Domain.Interfaces.Readers;

namespace LibrariansWorkplace.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository BookRepository { get; }
    IReaderRepository ReaderRepository { get; }
    IIssuedBooksRepository IssuedBooksRepository { get; }

    void Commit();
    void Rollback();
}
