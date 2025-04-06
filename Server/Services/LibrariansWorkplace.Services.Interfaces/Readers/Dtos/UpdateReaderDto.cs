namespace LibrariansWorkplace.Services.Interfaces.Readers.Dtos;

public class UpdateReaderDto
{
    public required string FullName { get; set; } 
    public required DateTime DateBirth { get; set; }
}
