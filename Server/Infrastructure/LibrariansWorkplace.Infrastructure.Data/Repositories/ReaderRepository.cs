using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces.Books;
using Microsoft.EntityFrameworkCore;

namespace LibrariansWorkplace.Infrastructure.Data.Repositories;

public class ReaderRepository : IReaderRepository
{
    private readonly AppDbContext _context;

    public ReaderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Reader reader)
    {
        await _context.Readers.AddAsync(reader);
    }

    public Task<Reader?> Get(int id)
    {
        return _context.Readers.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
    }

    public Task<Reader?> GetFull(int id)
    {
        return _context.Readers
            .Include(x => x.IssuedBooks).ThenInclude(x => x.Book)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
    }

    public async Task<IEnumerable<Reader>> GetAll()
    {
        return await _context.Readers.Where(x => x.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Reader>> SearchByFullName(string search)
    {
        return await _context.Readers
            .Include(x => x.IssuedBooks).ThenInclude(x => x.Book)
            .Where(x => x.IsDeleted == false && x.FullName.Contains(search, StringComparison.CurrentCultureIgnoreCase))
            .ToListAsync();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}

