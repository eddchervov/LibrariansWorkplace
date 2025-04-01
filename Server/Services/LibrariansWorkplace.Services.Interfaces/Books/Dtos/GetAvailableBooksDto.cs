namespace LibrariansWorkplace.Services.Interfaces.Books.Dtos;

public class GetAvailableBooksDto
{
    public required int Id { get; set; }
    public required string Name { get; set; } = default!;
    public required string Author { get; set; } = default!;
    public required int YearPublication { get; set; }
    public required int CountCopies { get; set; }
    public required int CountAvailableBooks { get; set; }
}
