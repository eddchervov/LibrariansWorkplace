using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Services.Interfaces.Readers.Dtos;
using System.Linq.Expressions;

namespace LibrariansWorkplace.Infrastructure.BL.Readers.Expressions;


public class ExpressionHelper
{
    public static readonly Expression<Func<Reader, GetReaderDto>> MapToGetReaderDtoExpr = reader => new GetReaderDto
    {
        Id = reader.Id,
        FullName = reader.FullName,
        DateBirth = reader.DateBirth,
        // PS отдаем и повыдачивторяющиеся, так как у них разные даты 
        IssuedBooks = reader.IssuedBooks.Where(x => x.IsDeleted == false).Select(issuedBook => new BookGivenToReaderDto 
        {
            Book = new ReaderBookDto
            {
                Id = issuedBook.BookId,
                Author = issuedBook.Book.Author,
                Name = issuedBook.Book.Name,
                YearPublication = issuedBook.Book.YearPublication
            },
            Count = issuedBook.Count,
            DateOfIssue = issuedBook.DateOfIssue
        })
    };

    public static readonly Expression<Func<Reader, ReaderOptionDto>> MapToReaderOptionDtoExpr = reader => new ReaderOptionDto
    {
        Id = reader.Id,
        Name = reader.FullName,
        DateBirth = reader.DateBirth
    };


    public static readonly Expression<Func<Reader, Reader>> MapToReaderExpr = reader => reader;
}