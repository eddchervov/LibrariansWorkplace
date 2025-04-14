using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Infrastructure.BL.Books.Expressions;
using LibrariansWorkplace.Infrastructure.BL.Books.Validations;
using LibrariansWorkplace.Services.Interfaces.Books;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using LibrariansWorkplace.Services.Interfaces.Books.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LibrariansWorkplace.Infrastructure.BL.Books;

public class BookService(ISystemClock systemClock, IUnitOfWork unitOfWork) : IBookService
{
    private readonly ISystemClock _systemClock = systemClock;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<BookOptionDto>> GetOptions()
        => await _unitOfWork.BookRepository.Get(ExpressionHelper.MapToBookOptionDtoExpr).OrderBy(x => x.Name).ToListAsync();

    public async Task<GetBookDto> GetById(int bookId)
        => (await _unitOfWork.BookRepository.Get(bookId, ExpressionHelper.MapToGetBookDtoExpr))
        ?? throw new BookNotFoundException(bookId);

    public async Task<IEnumerable<GetBookDto>> SearchByName(string search)
    {
        if (string.IsNullOrEmpty(search.Trim())) return [];

        return await _unitOfWork.BookRepository
            .SearchByName(search)
            .Select(ExpressionHelper.MapToGetBookDtoExpr)
            .ToListAsync();
    }

    public async Task<IEnumerable<GetIssuedBooksDto>> GetIssuedBooks()
        => await _unitOfWork.BookRepository
            .Get(ExpressionHelper.MapToGetIssuedBooksDtoExpr)
            .Where(x => x.CountIssuedBooks > 0)
            .ToListAsync();

    public async Task<IEnumerable<GetAvailableBooksDto>> GetAvailableBooks()
        => await _unitOfWork.BookRepository
            .Get(ExpressionHelper.MapToGetAvailableBooksDtoExpr)
            .Where(x => x.CountAvailableBooks > 0)
            .ToListAsync();

    public async Task<int> Create(CreateBookDto dto)
    {
        ValidationHelper.ValidationOnCreation(dto, _systemClock);

        var book = new Book
        {
            Author = dto.Author,
            CountCopies = dto.CountCopies,
            Name = dto.Name,
            YearPublication = dto.YearPublication
        };

        await _unitOfWork.BookRepository.Add(book);

        await _unitOfWork.BookRepository.Save();

        return book.Id;
    }

    public async Task Update(int bookId, UpdateBookDto dto)
    {
        var book = (await _unitOfWork.BookRepository.Get(bookId, ExpressionHelper.MapToBookExpr)) 
            ?? throw new BookNotFoundException(bookId);
        var countBooksIssued = await _unitOfWork.IssuedBooksRepository
            .GetByBookId(book.Id)
            .SumAsync(x => x.Count);

        ValidationHelper.ValidationOnUpdate(dto, book, countBooksIssued, _systemClock);

        book.Author = dto.Author;
        book.CountCopies = dto.CountCopies;
        book.Name = dto.Name;
        book.YearPublication = dto.YearPublication;

        await _unitOfWork.BookRepository.Save();
    }

    public async Task Delete(int bookId)
    {
        var book = (await _unitOfWork.BookRepository.Get(bookId, ExpressionHelper.MapToBookExpr)) 
            ?? throw new BookNotFoundException(bookId);

        var existBooksIssued = await _unitOfWork.IssuedBooksRepository
           .GetByBookId(book.Id)
           .AnyAsync();

        if (existBooksIssued) throw new YouCanNotDeleteBookBecauseThereAreCopiesIssuedException(book.Id);

        book.IsDeleted = true;

        await _unitOfWork.BookRepository.Save();
    }
}
