﻿namespace LibrariansWorkplace.Services.Interfaces.Books.Dtos;

public class CreateBookDto
{
    public required string Name { get; set; } 
    public required string Author { get; set; } 
    public required int YearPublication { get; set; }
    public required int CountCopies { get; set; }
}
