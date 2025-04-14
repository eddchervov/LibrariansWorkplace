using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Infrastructure.BL.Books.Expressions;
using LibrariansWorkplace.Services.Interfaces.Books;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LibrariansWorkplace.Infrastructure.BL.Books;

public class BookManagementService(ISystemClock systemClock, IUnitOfWork unitOfWork) : IBookManagementService
{
    private readonly ISystemClock _systemClock = systemClock;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task GiveBookToReader(GiveBookToReaderDto dto)
    {
        // Добавляем пока что, только по 1 книги
        int countAdded = 1;

        if (await _unitOfWork.ReaderRepository.Any(dto.ReaderId) == false) throw new ReaderNotFoundException(dto.ReaderId);
        var book = await _unitOfWork.BookRepository.Get(dto.BookId, ExpressionHelper.MapToBookExpr) ?? throw new BookNotFoundException(dto.BookId);

        var countIssuedBooks = (await _unitOfWork.IssuedBooksRepository
            .GetByBookId(dto.BookId)
            .SumAsync(x => x.Count)) + countAdded;

        if (countIssuedBooks > book.CountCopies)
        {
            throw new ThereAreNoAvailableCopiesOfLookException(book.Id);
        }

        book.IssuedBooks.Add(new IssuedBook
        {
            BookId = book.Id,
            ReaderId = dto.ReaderId,
            Count = countAdded,
            DateOfIssue = _systemClock.UtcNow
        });

        await _unitOfWork.IssuedBooksRepository.Save();
    }

    public async Task ReturnBookToLibrary(ReturnBookToLibraryDto dto)
    {
        if (await _unitOfWork.BookRepository.Any(dto.BookId) == false) throw new BookNotFoundException(dto.BookId);
        if (await _unitOfWork.ReaderRepository.Any(dto.ReaderId) == false) throw new ReaderNotFoundException(dto.ReaderId);

        // берем любую попавшуюся
        var issuedBook = (await _unitOfWork.IssuedBooksRepository.GetBy(dto.ReaderId, dto.BookId)) ?? throw new BookNotFoundForReaderException(dto.BookId);

        issuedBook.IsDeleted = true;

        await _unitOfWork.IssuedBooksRepository.Save();
    }
}
