namespace LibrariansWorkplace.Services.Interfaces.Books.Dtos;

public class GetBookDto
{
    public required int Id { get; set; }
    public required string Name { get; set; } 
    public required string Author { get; set; }
    public required int YearPublication { get; set; }
    public required int CountCopies { get; set; }
}
