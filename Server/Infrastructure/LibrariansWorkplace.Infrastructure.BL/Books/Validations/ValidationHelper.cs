using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Helpers;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Services.Interfaces.Books.Dtos;
using LibrariansWorkplace.Services.Interfaces.Books.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;

namespace LibrariansWorkplace.Infrastructure.BL.Books.Validations;

public class ValidationHelper
{
    public static void ValidationOnUpdate(UpdateBookDto dto, Book book, int countBooksIssued, ISystemClock systemClock)
    {
        if (countBooksIssued > book.CountCopies)
        {
            throw new IndicatedCountCopiesLessThanIssuedException(countBooksIssued, book.CountCopies);
        }

        ValidationName(dto.Name);
        ValidationAuthor(dto.Author);
        ValidationCountCopies(dto.CountCopies);
        ValidationYearPublication(dto.YearPublication, systemClock);
    }

    public static void ValidationOnCreation(CreateBookDto dto, ISystemClock systemClock)
    {
        ValidationName(dto.Name);
        ValidationAuthor(dto.Author);
        ValidationCountCopies(dto.CountCopies);
        ValidationYearPublication(dto.YearPublication, systemClock);
    }

    private static void ValidationName(string name)
    {
        if (string.IsNullOrEmpty(name.Trim()))
        {
            throw new EmptyValueException(nameof(name));
        }
    }

    private static void ValidationAuthor(string author)
    {
        if (string.IsNullOrEmpty(author.Trim()))
        {
            throw new EmptyValueException(nameof(author));
        }
    }

    private static void ValidationCountCopies(int countCopies)
    {
        if (countCopies < BookSetup.MinCountCopies || countCopies > BookSetup.MaxCountCopies)
        {
            throw new CountCopiesOutOfRangeException(countCopies);
        }
    }

    private static void ValidationYearPublication(int yearPublication, ISystemClock systemClock)
    {
        if (yearPublication < BookSetup.MinYearPublication || yearPublication > systemClock.UtcNow.Year)
        {
            throw new YearPublicationOutOfRangeException(yearPublication, systemClock.UtcNow.Year);
        }
    }
}
