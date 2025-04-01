namespace LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

public class BookGivenToReaderDto
{
    public required ReaderBookDto Book { get; set; }

    public int Count { get; set; }

    public DateTime DateOfIssue { get; set; }
}
