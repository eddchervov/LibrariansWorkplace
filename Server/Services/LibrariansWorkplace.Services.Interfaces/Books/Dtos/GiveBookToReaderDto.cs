namespace LibrariansWorkplace.Services.Interfaces.Books.Dtos;

public class GiveBookToReaderDto
{
    public required int BookId { get; set; }
    public required int ReaderId { get; set; }
}
