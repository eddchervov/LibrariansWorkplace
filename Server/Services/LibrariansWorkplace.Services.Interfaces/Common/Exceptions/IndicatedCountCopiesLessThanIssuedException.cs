namespace LibrariansWorkplace.Services.Interfaces.Common.Exceptions;

public class IndicatedCountCopiesLessThanIssuedException : Exception
{
    public IndicatedCountCopiesLessThanIssuedException(int countIssuedBooks, int countCopies) 
        : base($"Выставлено значений экземпляров {countCopies} меньше чем уже выдано - {countIssuedBooks}") { }
}