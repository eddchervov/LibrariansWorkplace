using LibrariansWorkplace.Services.Interfaces.Books.Dtos;

namespace LibrariansWorkplace.Services.Interfaces.Books;

public interface IBookManagementService
{
    Task GiveBookToReader(GiveBookToReaderDto dto);
    Task ReturnBookToLibrary(ReturnBookToLibraryDto dto);
}
