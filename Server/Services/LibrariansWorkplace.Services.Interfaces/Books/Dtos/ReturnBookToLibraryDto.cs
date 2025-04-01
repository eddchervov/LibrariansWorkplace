namespace LibrariansWorkplace.Services.Interfaces.Books.Dtos;

public class ReturnBookToLibraryDto
{
    public required int BookId { get; set; }
    public required int ReaderId { get; set; }
}
