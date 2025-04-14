namespace LibrariansWorkplace.Domain;

public class Reader
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public DateTime DateBirth { get; set; }

    public List<IssuedBook> IssuedBooks { get; set; } = [];
    public bool IsDeleted { get; set; }
}
