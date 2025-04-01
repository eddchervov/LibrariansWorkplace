using LibrariansWorkplace.Services.Interfaces.Readers;
using LibrariansWorkplace.Services.Interfaces.Readers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibrariansWorkplace.Web.Controllers;

[Route("readers")]
[ApiController]
public class ReaderController : ControllerBase
{
    private readonly IReaderService _readerService;

    public ReaderController(IReaderService readerService)
    {
        _readerService = readerService;
    }

    [HttpGet]
    public async Task<GetReaderDto> GetById(int readerId)
    {
        return await _readerService.GetById(readerId);
    }

    [HttpGet("search-by-full-name")]
    public async Task<IEnumerable<GetReaderDto>> SearchByFullName(string search)
    {
        return await _readerService.SearchByFullName(search);
    }

    [HttpPost]
    public async Task<int> Create([FromBody] CreateReaderDto request)
    {
        return await _readerService.Create(request);
    }

    [HttpPut("{readerId}")]
    public async Task Update(int readerId, [FromBody] UpdateReaderDto request)
    {
        await _readerService.Update(readerId, request);
    }

    [HttpDelete("{readerId}")]
    public async Task Delete(int readerId)
    {
        await _readerService.Delete(readerId);
    }
}
