using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Readers;
using LibrariansWorkplace.Services.Interfaces.Readers.Dtos;
using LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;

namespace LibrariansWorkplace.Infrastructure.BL.Readers;

public class ReaderService : IReaderService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReaderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetReaderDto> GetById(int id)
    {
        var reader = (await _unitOfWork.ReaderRepository.Get(id)) ?? throw new ReaderNotFoundException(id);

        var issuedBooks = reader.IssuedBooks.Where(x => x.IsDeleted == false);

        return MapToDto(reader);
    }

    public async Task<IEnumerable<GetReaderDto>> SearchByFullName(string search)
    {
        if (string.IsNullOrEmpty(search)) return Enumerable.Empty<GetReaderDto>();

        var readers = await _unitOfWork.ReaderRepository.SearchByFullName(search);

        return readers.Select(MapToDto);
    }

    public async Task<int> Create(CreateReaderDto dto)
    {
        ValidationOnCreation(dto);

        var reader = new Reader
        {
            DateBirth = dto.DateBirth,
            FullName = dto.FullName
        };

        await _unitOfWork.ReaderRepository.Add(reader);

        await _unitOfWork.ReaderRepository.Save();

        return reader.Id;
    }

    public async Task Update(int readerId, UpdateReaderDto dto)
    {
        ValidationOnUpdate(dto);

        var book = (await _unitOfWork.ReaderRepository.Get(readerId)) ?? throw new ReaderNotFoundException(readerId);

        book.DateBirth = dto.DateBirth;
        book.FullName = dto.FullName;

        await _unitOfWork.ReaderRepository.Save();
    }

    public async Task Delete(int id)
    {
        var book = (await _unitOfWork.ReaderRepository.Get(id)) ?? throw new ReaderNotFoundException(id);

        book.IsDeleted = true;

        await _unitOfWork.ReaderRepository.Save();
    }

    private static GetReaderDto MapToDto(Reader reader)
    {
        var issuedBooks = reader.IssuedBooks.Where(x => x.IsDeleted == false);

        return new GetReaderDto
        {
            Id = reader.Id,
            DateBirth = reader.DateBirth,
            FullName = reader.FullName,
            IsDeleted = reader.IsDeleted,
            IssuedBooks = issuedBooks.Select(MapToDto) // PS отдаем и повторяющиеся, так как у них разные даты выдачи
        };
    }

    private static BookGivenToReaderDto MapToDto(IssuedBook issuedBook)
    {
        return new BookGivenToReaderDto
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
        };
    } 

    private void ValidationOnUpdate(UpdateReaderDto dto)
    {
        ValidationName(dto.FullName);
        ValidationDateBirth(dto.DateBirth);
    }

    private void ValidationOnCreation(CreateReaderDto dto)
    {
        ValidationName(dto.FullName);
        ValidationDateBirth(dto.DateBirth);
    }

    private static void ValidationName(string name)
    {
        if (string.IsNullOrEmpty(name.Trim()))
        {
            throw new EmptyValueException(nameof(name));
        }
    }

    private void ValidationDateBirth(DateTime dateBirth)
    {
        if (dateBirth < Reader.MinDateBirth || dateBirth > Reader.MaxDateBirth)
        {
            throw new DateBirthOutOfRangeException(dateBirth);
        }
    }
}
