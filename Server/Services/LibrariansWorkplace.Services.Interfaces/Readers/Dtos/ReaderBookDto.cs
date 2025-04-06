namespace LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

public class ReaderBookDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; } 
    public required int YearPublication { get; set; }
}
