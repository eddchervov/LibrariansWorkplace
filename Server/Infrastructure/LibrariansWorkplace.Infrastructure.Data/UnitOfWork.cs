using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Domain.Interfaces.Books;
using LibrariansWorkplace.Domain.Interfaces.IssuedBooks;
using LibrariansWorkplace.Domain.Interfaces.Readers;
using LibrariansWorkplace.Infrastructure.Data.Repositories;

namespace LibrariansWorkplace.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IBookRepository _bookRepository;
    private IReaderRepository _readerRepository;
    private IIssuedBooksRepository _issuedBooksRepository;

#nullable disable
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
#nullable enable

    public IBookRepository BookRepository
    {
        get { return _bookRepository ??= new BookRepository(_context); }
    }

    public IReaderRepository ReaderRepository
    {
        get { return _readerRepository ??= new ReaderRepository(_context); }
    }

    public IIssuedBooksRepository IssuedBooksRepository
    {
        get { return _issuedBooksRepository ??= new IssuedBooksRepository(_context); }
    }

    public void Commit()
    {
        _context.SaveChanges();
        _context.CommitTransaction();
    }

    public void Rollback()
    {
        _context.RollbackTransaction();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _context?.Dispose();
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}