using LibrariansWorkplace.Services.Interfaces.Books;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibrariansWorkplace.Web.Controllers;

[Route("book-management")]
[ApiController]
public class BookManagementController : ControllerBase
{
    private readonly IBookManagementService _bookManagementService;

    public BookManagementController(IBookManagementService bookManagementService)
    {
        _bookManagementService = bookManagementService;
    }

    [HttpPost("give-book-to-reader")]
    public async Task GiveBookToReader([FromBody] GiveBookToReaderDto dto)
    {
        await _bookManagementService.GiveBookToReader(dto);
    }

    [HttpPost("return-book-to-library")]
    public async Task ReturnBookToLibrary([FromBody] ReturnBookToLibraryDto dto)
    {
        await _bookManagementService.ReturnBookToLibrary(dto);
    }
}