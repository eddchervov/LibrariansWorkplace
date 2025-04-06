namespace LibrariansWorkplace.Domain;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Author { get; set; } = default!;
    public int YearPublication { get; set; }
    public int CountCopies { get; set; }

    public List<IssuedBook> IssuedBooks { get; set; } = [];

    public bool IsDeleted { get; set; }


    public static int MinCountCopies = 1;
    public static int MaxCountCopies = 10000000;

    public static int MinYearPublication = 1;
}
