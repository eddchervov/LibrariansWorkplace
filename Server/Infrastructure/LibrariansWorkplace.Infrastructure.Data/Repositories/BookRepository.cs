using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces.Books;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibrariansWorkplace.Infrastructure.Data.Repositories;

public class BookRepository(AppDbContext context) : IBookRepository
{
    private readonly AppDbContext _context = context;

    public Task<T?> Get<T>(int id, Expression<Func<Book, T>> mapExpr) where T: class
    {
        return _context.Books
            .Where(x => x.Id == id && x.IsDeleted == false)
            .Select(mapExpr)
            .FirstOrDefaultAsync();
    }

    public IQueryable<T> Get<T>(Expression<Func<Book, T>> mapExpr) where T : class
    {
        return _context.Books
            .Where(x => x.IsDeleted == false)
            .Select(mapExpr);
    }

    public async Task<bool> Any(int id)
    {
        return await _context.Books.AnyAsync(x => x.Id == id && x.IsDeleted == false);
    }

    public IQueryable<Book> SearchByName(string search)
    {
        var localSearch = search.Trim().ToLower();
        return _context.Books.Where(x => x.IsDeleted == false && EF.Functions.ILike(x.Name, $"%{localSearch}%"));
    }

    public async Task Add(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
