using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces.Books;
using Microsoft.EntityFrameworkCore;

namespace LibrariansWorkplace.Infrastructure.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public Task<Book?> Get(int id)
    {
        return _context.Books
            .Include(x => x.IssuedBooks).ThenInclude(x => x.Reader)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
    }

    public async Task<IEnumerable<Book>> Get()
    {
        return await _context.Books
            .Include(x => x.IssuedBooks)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> SearchByName(string search)
    {
        var localSearch = search.Trim().ToLower();
        return await _context.Books
            .Include(x => x.IssuedBooks).ThenInclude(x => x.Book)
            .Where(x => x.IsDeleted == false && x.Name.ToLower().Contains(localSearch))
            .ToListAsync();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
