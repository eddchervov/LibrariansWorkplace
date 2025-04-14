using System.Linq.Expressions;

namespace LibrariansWorkplace.Domain.Interfaces.Books;

public interface IBookRepository
{
    Task<T?> Get<T>(int id, Expression<Func<Book, T>> mapExpr) where T : class;
    IQueryable<T> Get<T>(Expression<Func<Book, T>> mapExpr) where T : class;
    Task<bool> Any(int id);
    IQueryable<Book> SearchByName(string search);
    Task Add(Book book);
    Task Save();
}
