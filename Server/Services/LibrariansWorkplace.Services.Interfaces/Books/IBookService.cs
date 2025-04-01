using LibrariansWorkplace.Services.Interfaces.Books.Dtos;

namespace LibrariansWorkplace.Services.Interfaces.Books;

public interface IBookService
{
    Task<GetBookDto> GetById(int id);
    Task<IEnumerable<GetIssuedBooksDto>> GetIssuedBooks();
    Task<IEnumerable<GetAvailableBooksDto>> GetAvailableBooks();
    Task<IEnumerable<GetBookDto>> SearchByName(string search);
    Task<int> Create(CreateBookDto dto);
    Task Update(int bookId, UpdateBookDto dto);
    Task Delete(int id);
}
