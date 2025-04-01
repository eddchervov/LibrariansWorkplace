using LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

namespace LibrariansWorkplace.Services.Interfaces.Readers;

public interface IReaderService
{
    Task<GetReaderDto> GetById(int id);
    Task<IEnumerable<GetReaderDto>> SearchByFullName(string search);
    Task<int> Create(CreateReaderDto dto);
    Task Update(int readerId, UpdateReaderDto dto);
    Task Delete(int id);
}