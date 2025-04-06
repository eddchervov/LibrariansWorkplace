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

    [HttpGet("options")]
    public async Task<IEnumerable<ReaderOptionDto>> GetOptions()
    {
        return await _readerService.GetOptions();
    }

    [HttpGet("{readerId}")]
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
        try
        {

       
        return await _readerService.Create(request);
        }
        catch (Exception ex)
        {

            throw;
        }
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
