using LibrariansWorkplace.Services.Interfaces.Books;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibrariansWorkplace.Web.Controllers;

[Route("books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService) 
    {
        _bookService = bookService;
    }

    [HttpGet("options")]
    public async Task<IEnumerable<BookOptionDto>> GetOptions()
    {
        return await _bookService.GetOptions();
    }

    [HttpGet("{bookId}")]
    public async Task<GetBookDto> GetById(int bookId)
    {
        return await _bookService.GetById(bookId);
    }

    [HttpGet("search-by-name")]
    public async Task<IEnumerable<GetBookDto>> SearchByName(string search)
    {
        return await _bookService.SearchByName(search);
    }

    [HttpGet("issued-books")]
    public async Task<IEnumerable<GetIssuedBooksDto>> GetIssuedBooks()
    {
        return await _bookService.GetIssuedBooks();
    }

    [HttpGet("available-books")]
    public async Task<IEnumerable<GetAvailableBooksDto>> GetAvailableBooks()
    {
        return await _bookService.GetAvailableBooks();
    }

    [HttpPost]
    public async Task<int> Create([FromBody] CreateBookDto request)
    {
        return await _bookService.Create(request);
    }

    [HttpPut("{bookId}")]
    public async Task Update(int bookId, [FromBody] UpdateBookDto request)
    {
        await _bookService.Update(bookId, request);
    }

    [HttpDelete("{bookId}")]
    public async Task Delete(int bookId)
    {
        await _bookService.Delete(bookId);
    }
}
