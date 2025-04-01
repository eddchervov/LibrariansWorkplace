namespace LibrariansWorkplace.Domain;

public class IssuedBook
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = default!;

    public int ReaderId { get; set; }
    public Reader Reader { get; set; } = default!;

    public int Count { get; set; }

    public DateTime DateOfIssue { get; set; }

    public bool IsDeleted { get; set; }
}
