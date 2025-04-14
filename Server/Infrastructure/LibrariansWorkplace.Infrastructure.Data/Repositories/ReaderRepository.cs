using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces.Readers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibrariansWorkplace.Infrastructure.Data.Repositories;

public class ReaderRepository(AppDbContext context) : IReaderRepository
{
    private readonly AppDbContext _context = context;

    public Task<T?> Get<T>(int id, Expression<Func<Reader, T>> mapExpr) where T : class
    {
        return _context.Readers
            .Where(x => x.Id == id && x.IsDeleted == false)
            .Select(mapExpr)
            .FirstOrDefaultAsync();
    }

    public IQueryable<T> Get<T>(Expression<Func<Reader, T>> mapExpr) where T : class
    {
        return _context.Readers
            .Where(x => x.IsDeleted == false)
            .Select(mapExpr);
    }

    public async Task<bool> Any(int id)
    {
        return await _context.Readers.AnyAsync(x => x.Id == id && x.IsDeleted == false);
    }

    public IQueryable<Reader> SearchByFullName(string search)
    {
        var localSearch = search.Trim().ToLower();
        return _context.Readers.Where(x => x.IsDeleted == false && EF.Functions.ILike(x.FullName, $"%{localSearch}%"));
    }

    public async Task Add(Reader reader)
    {
        await _context.Readers.AddAsync(reader);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}

