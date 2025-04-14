using System.Linq.Expressions;

namespace LibrariansWorkplace.Domain.Interfaces.Readers;

public interface IReaderRepository
{
    Task<T?> Get<T>(int id, Expression<Func<Reader, T>> mapExpr) where T : class;
    IQueryable<T> Get<T>(Expression<Func<Reader, T>> mapExpr) where T : class;
    Task<bool> Any(int id);
    IQueryable<Reader> SearchByFullName(string search);
    Task Add(Reader book);
    Task Save();
}