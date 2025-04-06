namespace LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

public class BookGivenToReaderDto
{
    public required ReaderBookDto Book { get; set; }

    public required int Count { get; set; }

    public required DateTime DateOfIssue { get; set; }
}
