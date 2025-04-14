using LibrariansWorkplace.Domain.Helpers;
using LibrariansWorkplace.Services.Interfaces.Common.Exceptions;
using LibrariansWorkplace.Services.Interfaces.Readers.Dtos;
using LibrariansWorkplace.Services.Interfaces.Readers.Exceptions;

namespace LibrariansWorkplace.Infrastructure.BL.Readers.Validations;

public class ValidationHelper
{
    public static void ValidationOnUpdate(UpdateReaderDto dto)
    {
        ValidationName(dto.FullName);
        ValidationDateBirth(dto.DateBirth);
    }

    public static void ValidationOnCreation(CreateReaderDto dto)
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
        if (dateBirth < ReaderSetup.MinDateBirth || dateBirth > ReaderSetup.MaxDateBirth)
        {
            throw new DateBirthOutOfRangeException(dateBirth);
        }
    }
}
