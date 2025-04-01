using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrariansWorkplace.Services.Interfaces.Books.Dtos;

public class GetIssuedBooksDto
{
    public required int Id { get; set; }
    public required string Name { get; set; } = default!;
    public required string Author { get; set; } = default!;
    public required int YearPublication { get; set; }
    public required int CountCopies { get; set; }
    public required int CountIssuedBooks { get; set; }
}
