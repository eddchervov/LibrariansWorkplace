namespace LibrariansWorkplace.Domain.Interfaces.IssuedBooks;

public interface IIssuedBooksRepository
{
    IQueryable<IssuedBook> GetByBookId(int bookId);
    IQueryable<IssuedBook> GetByReaderId(int readerId);
    Task<IssuedBook?> GetBy(int readerId, int bookId);
    Task Add(IssuedBook book);
    Task Save();
}