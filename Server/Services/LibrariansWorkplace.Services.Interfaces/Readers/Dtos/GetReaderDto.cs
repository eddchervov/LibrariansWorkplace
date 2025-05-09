﻿namespace LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

public class GetReaderDto
{
    public required int Id { get; set; }
    public required string FullName { get; set; }
    public required DateTime DateBirth { get; set; }

    public required IEnumerable<BookGivenToReaderDto> IssuedBooks { get; set; }
}
