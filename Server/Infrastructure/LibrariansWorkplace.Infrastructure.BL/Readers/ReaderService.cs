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

    public async Task<IEnumerable<ReaderOptionDto>> GetOptions()
    {
        var readers = await _unitOfWork.ReaderRepository.GetAll();
        return readers.Select(x => new ReaderOptionDto { Id = x.Id, Name = x.FullName, DateBirth = x.DateBirth }).OrderBy(x => x.Name);
    }

    public async Task<GetReaderDto> GetById(int id)
    {
        var reader = (await _unitOfWork.ReaderRepository.GetFull(id)) ?? throw new ReaderNotFoundException(id);

        var issuedBooks = reader.IssuedBooks.Where(x => x.IsDeleted == false);

        return MapToDto(reader);
    }

    public async Task<IEnumerable<GetReaderDto>> SearchByFullName(string search)
    {
        if (string.IsNullOrEmpty(search.Trim())) return [];

        var readers = await _unitOfWork.ReaderRepository.SearchByFullName(search);

        return readers.Select(MapToDto);
    }

    public async Task<int> Create(CreateReaderDto dto)
    {
        ValidationOnCreation(dto);

        var reader = new Reader
        {
            DateBirth = dto.DateBirth.ToUniversalTime(),
            FullName = dto.FullName
        };

        await _unitOfWork.ReaderRepository.Add(reader);

        await _unitOfWork.ReaderRepository.Save();

        return reader.Id;
    }

    public async Task Update(int readerId, UpdateReaderDto dto)
    {
        ValidationOnUpdate(dto);

        var reader = (await _unitOfWork.ReaderRepository.Get(readerId)) ?? throw new ReaderNotFoundException(readerId);

        reader.DateBirth = dto.DateBirth.ToUniversalTime();
        reader.FullName = dto.FullName;

        await _unitOfWork.ReaderRepository.Save();
    }

    public async Task Delete(int id)
    {
        var reader = (await _unitOfWork.ReaderRepository.GetFull(id)) ?? throw new ReaderNotFoundException(id);

        if (reader.IssuedBooks.Any(x => x.IsDeleted == false))
        {
            throw new YouCannotRemoveReaderBecauseThereAreUndeliveredBooksException(reader.Id);
        }

        reader.IsDeleted = true;

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

    private static void ValidationOnUpdate(UpdateReaderDto dto)
    {
        ValidationName(dto.FullName);
        ValidationDateBirth(dto.DateBirth);
    }

    private static void ValidationOnCreation(CreateReaderDto dto)
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

    private static void ValidationDateBirth(DateTime dateBirth)
    {
        if (dateBirth < Reader.MinDateBirth || dateBirth > Reader.MaxDateBirth)
        {
            throw new DateBirthOutOfRangeException(dateBirth);
        }
    }
}
