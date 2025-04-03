using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Services.Interfaces.Books;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using LibrariansWorkplace.Services.Interfaces.Books.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;

namespace LibrariansWorkplace.Infrastructure.BL.Books;

public class BookService : IBookService
{
    private readonly ISystemClock _systemClock;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(ISystemClock systemClock, IUnitOfWork unitOfWork)
    {
        _systemClock = systemClock;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetBookDto> GetById(int id)
    {
        var book = (await _unitOfWork.BookRepository.Get(id)) ?? throw new BookNotFoundException(id);

        return MapToDto(book);
    }

    public async Task<IEnumerable<GetBookDto>> SearchByName(string search)
    {
        if (string.IsNullOrEmpty(search)) return [];

        var books = await _unitOfWork.BookRepository.SearchByName(search);

        return books.Select(MapToDto);
    }

    public async Task<IEnumerable<GetIssuedBooksDto>> GetIssuedBooks()
    {
        var books = await _unitOfWork.BookRepository.GetFull();

        var result = new List<GetIssuedBooksDto>();
        foreach (var book in books) 
        {
            var countIssuedBooks = book.IssuedBooks.Where(x => x.IsDeleted == false).Sum(x => x.Count);
            if (countIssuedBooks > 0)
            {
                result.Add(new GetIssuedBooksDto
                {
                    Id = book.Id,
                    Author = book.Author,
                    CountCopies = book.CountCopies,
                    CountIssuedBooks = countIssuedBooks,
                    Name = book.Name,
                    YearPublication = book.YearPublication
                });
            }
        }

        return result;
    }

    public async Task<IEnumerable<GetAvailableBooksDto>> GetAvailableBooks()
    {
        var books = await _unitOfWork.BookRepository.GetFull();

        var result = new List<GetAvailableBooksDto>();
        foreach (var book in books)
        {
            var countIssuedBooks = book.IssuedBooks.Where(x => x.IsDeleted == false).Sum(x => x.Count);
            var countAvailableBooks = book.CountCopies - countIssuedBooks;

            if (countAvailableBooks > 0)
            {
                result.Add(new GetAvailableBooksDto
                {
                    Id = book.Id,
                    Author = book.Author,
                    CountCopies = book.CountCopies,
                    CountAvailableBooks = countAvailableBooks,
                    Name = book.Name,
                    YearPublication = book.YearPublication
                });
            }
        }

        return result;
    }

    public async Task<int> Create(CreateBookDto dto)
    {
        ValidationOnCreation(dto);

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
        var book = (await _unitOfWork.BookRepository.GetFull(bookId)) ?? throw new BookNotFoundException(bookId);

        ValidationOnUpdate(dto, book);

        book.Author = dto.Author;
        book.CountCopies = dto.CountCopies;
        book.Name = dto.Name;
        book.YearPublication = dto.YearPublication;

        await _unitOfWork.BookRepository.Save();
    }

    public async Task Delete(int id)
    {
        var book = (await _unitOfWork.BookRepository.GetFull(id)) ?? throw new BookNotFoundException(id);

        if (book.IssuedBooks.Any(x => x.IsDeleted == false))
        {
            throw new YouCanNotDeleteBookBecauseThereAreCopiesIssuedException(book.Id);
        }

        book.IsDeleted = true;

        await _unitOfWork.BookRepository.Save();
    }

    private static GetBookDto MapToDto(Book book)
    {
        return new GetBookDto
        {
            Id = book.Id,
            Author = book.Author,
            CountCopies = book.CountCopies,
            Name = book.Name,
            YearPublication = book.YearPublication
        };
    }

    private void ValidationOnUpdate(UpdateBookDto dto, Book book)
    {
        var countBooksIssued = book.IssuedBooks.Where(x => x.IsDeleted == false).Sum(x => x.Count);

        if (countBooksIssued > book.CountCopies)
        {
            throw new IndicatedCountCopiesLessThanIssuedException(countBooksIssued, book.CountCopies);
        }

        ValidationName(dto.Name);
        ValidationAuthor(dto.Author);
        ValidationCountCopies(dto.CountCopies);
        ValidationYearPublication(dto.YearPublication);
    }

    private void ValidationOnCreation(CreateBookDto dto)
    {
        ValidationName(dto.Name);
        ValidationAuthor(dto.Author);
        ValidationCountCopies(dto.CountCopies);
        ValidationYearPublication(dto.YearPublication);
    }

    private static void ValidationName(string name)
    {
        if (string.IsNullOrEmpty(name.Trim()))
        {
            throw new EmptyValueException(nameof(name));
        }
    }

    private static void ValidationAuthor(string author)
    {
        if (string.IsNullOrEmpty(author.Trim()))
        {
            throw new EmptyValueException(nameof(author));
        }
    }

    private static void ValidationCountCopies(int countCopies)
    {
        if (countCopies < Book.MinCountCopies || countCopies > Book.MaxCountCopies)
        {
            throw new CountCopiesOutOfRangeException(countCopies);
        }
    }

    private void ValidationYearPublication(int yearPublication)
    {
        if (yearPublication < Book.MinYearPublication || yearPublication > _systemClock.UtcNow.Year)
        {
            throw new YearPublicationOutOfRangeException(yearPublication, _systemClock.UtcNow.Year);
        }
    }
}
