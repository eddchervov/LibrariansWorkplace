namespace LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

public class ReaderBookDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Author { get; set; } = default!;
    public int YearPublication { get; set; }
}
