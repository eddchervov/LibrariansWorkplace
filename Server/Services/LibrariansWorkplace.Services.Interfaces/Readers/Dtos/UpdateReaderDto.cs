namespace LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

public class UpdateReaderDto
{
    public required string FullName { get; set; } = default!;
    public required DateTime DateBirth { get; set; }
}
