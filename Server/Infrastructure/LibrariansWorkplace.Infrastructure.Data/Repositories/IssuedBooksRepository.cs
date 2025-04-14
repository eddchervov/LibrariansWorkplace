using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces.IssuedBooks;
using Microsoft.EntityFrameworkCore;

namespace LibrariansWorkplace.Infrastructure.Data.Repositories;

public class IssuedBooksRepository(AppDbContext context) : IIssuedBooksRepository
{
    private readonly AppDbContext _context = context;

    public async Task Add(IssuedBook issuedBook)
    {
        await _context.IssuedBooks.AddAsync(issuedBook);
    }

    public IQueryable<IssuedBook> GetByBookId(int bookId)
    {
        return _context.IssuedBooks.Where(x => x.BookId == bookId && x.IsDeleted == false);
    }

    public IQueryable<IssuedBook> GetByReaderId(int readerId)
    {
        return _context.IssuedBooks.Where(x => x.ReaderId == readerId && x.IsDeleted == false);
    }

    public Task<IssuedBook?> GetBy(int readerId, int bookId)
    {
        return _context.IssuedBooks.FirstOrDefaultAsync(x => x.ReaderId == readerId && x.BookId == bookId && x.IsDeleted == false);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
