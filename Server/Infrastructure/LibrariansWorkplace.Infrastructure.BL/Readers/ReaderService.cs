using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Infrastructure.BL.Readers.Expressions;
using LibrariansWorkplace.Infrastructure.BL.Readers.Validations;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Readers;
using LibrariansWorkplace.Services.Interfaces.Readers.Dtos;
using LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LibrariansWorkplace.Infrastructure.BL.Readers;

public class ReaderService(IUnitOfWork unitOfWork) : IReaderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<ReaderOptionDto>> GetOptions()
        => await _unitOfWork.ReaderRepository.Get(ExpressionHelper.MapToReaderOptionDtoExpr).OrderBy(x => x.Name).ToListAsync();
    
    public async Task<GetReaderDto> GetById(int readerId)
        => (await _unitOfWork.ReaderRepository.Get(readerId, ExpressionHelper.MapToGetReaderDtoExpr))
            ?? throw new ReaderNotFoundException(readerId);

    public async Task<IEnumerable<GetReaderDto>> SearchByFullName(string search)
    {
        if (string.IsNullOrEmpty(search.Trim())) return [];

        return await _unitOfWork.ReaderRepository
            .SearchByFullName(search)
            .Select(ExpressionHelper.MapToGetReaderDtoExpr)
            .ToListAsync();
    }

    public async Task<int> Create(CreateReaderDto dto)
    {
        ValidationHelper.ValidationOnCreation(dto);

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
        ValidationHelper.ValidationOnUpdate(dto);

        var reader = (await _unitOfWork.ReaderRepository.Get(readerId, ExpressionHelper.MapToReaderExpr)) 
            ?? throw new ReaderNotFoundException(readerId);

        reader.DateBirth = dto.DateBirth.ToUniversalTime();
        reader.FullName = dto.FullName;

        await _unitOfWork.ReaderRepository.Save();
    }

    public async Task Delete(int readerid)
    {
        var reader = (await _unitOfWork.ReaderRepository.Get(readerid, ExpressionHelper.MapToReaderExpr)) 
            ?? throw new ReaderNotFoundException(readerid);

        var existIssuedBooks = await _unitOfWork.IssuedBooksRepository
            .GetByBookId(reader.Id)
            .AnyAsync();

        if (existIssuedBooks) throw new YouCannotRemoveReaderBecauseThereAreUndeliveredBooksException(reader.Id);
        
        reader.IsDeleted = true;

        await _unitOfWork.ReaderRepository.Save();
    }
}