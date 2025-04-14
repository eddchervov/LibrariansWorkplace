using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using System.Linq.Expressions;

namespace LibrariansWorkplace.Infrastructure.BL.Books.Expressions;

public class ExpressionHelper
{
    public static readonly Expression<Func<Book, GetBookDto>> MapToGetBookDtoExpr = book => new GetBookDto
    {
        Id = book.Id,
        Name = book.Name,
        Author = book.Author,
        CountCopies = book.CountCopies,
        YearPublication = book.YearPublication
    };

    public static readonly Expression<Func<Book, GetIssuedBooksDto>> MapToGetIssuedBooksDtoExpr = book => new GetIssuedBooksDto
    {
        Id = book.Id,
        Author = book.Author,
        CountCopies = book.CountCopies,
        CountIssuedBooks = book.IssuedBooks.Where(x => x.IsDeleted == false).Sum(x => x.Count),
        Name = book.Name,
        YearPublication = book.YearPublication
    };

    public static readonly Expression<Func<Book, GetAvailableBooksDto>> MapToGetAvailableBooksDtoExpr = book => new GetAvailableBooksDto
    {
        Id = book.Id,
        Author = book.Author,
        CountCopies = book.CountCopies,
        CountAvailableBooks = book.CountCopies - book.IssuedBooks.Where(x => x.IsDeleted == false).Sum(x => x.Count),
        Name = book.Name,
        YearPublication = book.YearPublication
    };

    public static readonly Expression<Func<Book, BookOptionDto>> MapToBookOptionDtoExpr = book => new BookOptionDto
    {
        Id = book.Id,
        Name = $"{book.Name}, {book.Author}, {book.YearPublication} г."
    };


    public static readonly Expression<Func<Book, Book>> MapToBookExpr = book => book;
}


