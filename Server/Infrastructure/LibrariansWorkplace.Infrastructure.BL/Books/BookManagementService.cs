using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Services.Interfaces.Books;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;

namespace LibrariansWorkplace.Infrastructure.BL.Books;

public class BookManagementService : IBookManagementService
{
    private readonly ISystemClock _systemClock;
    private readonly IUnitOfWork _unitOfWork;

    public BookManagementService(ISystemClock systemClock, IUnitOfWork unitOfWork)
    {
        _systemClock = systemClock;
        _unitOfWork = unitOfWork;
    }

    public async Task GiveBookToReader(GiveBookToReaderDto dto)
    {
        // Добавляем пока что, только по 1 книги
        int countAdded = 1;

        var book = (await _unitOfWork.BookRepository.Get(dto.BookId) ?? throw new BookNotFoundException(dto.BookId));
        var reader = (await _unitOfWork.ReaderRepository.Get(dto.ReaderId)) ?? throw new ReaderNotFoundException(dto.ReaderId);

        var countIssuedBooks = book.IssuedBooks.Where(x => x.IsDeleted == false).Sum(x => x.Count) + countAdded;

        if (countIssuedBooks > book.CountCopies)
        {
            throw new ThereAreNoAvailableCopiesOfLookException(book.Id);
        }

        book.IssuedBooks.Add(new IssuedBook 
        {
            Book = book,
            BookId = book.Id,
            ReaderId = reader.Id,
            Reader = reader,
            Count = countAdded,
            DateOfIssue = _systemClock.UtcNow
        });

        await _unitOfWork.BookRepository.Save();
    }

    public async Task ReturnBookToLibrary(ReturnBookToLibraryDto dto)
    {
        var book = (await _unitOfWork.BookRepository.Get(dto.BookId) ?? throw new BookNotFoundException(dto.BookId));
        var reader = (await _unitOfWork.ReaderRepository.Get(dto.ReaderId)) ?? throw new ReaderNotFoundException(dto.ReaderId);

        // берем любую попавшуюся
        var issuedBook = reader.IssuedBooks.FirstOrDefault(x => x.BookId == book.Id && x.IsDeleted == false) 
            ?? throw new BookNotFoundForReaderException(dto.BookId);

        issuedBook.IsDeleted = true;

        await _unitOfWork.ReaderRepository.Save();
    }
}
