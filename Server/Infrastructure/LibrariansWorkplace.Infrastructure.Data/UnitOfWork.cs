using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Domain.Interfaces.Books;
using LibrariansWorkplace.Infrastructure.Data.Repositories;

namespace LibrariansWorkplace.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IBookRepository _bookRepository;
    private IReaderRepository _readerRepository;

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
        _context.Dispose();
    }
}