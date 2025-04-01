namespace LibrariansWorkplace.Services.Interfaces.Common.Exceptions;

public class EmptyValueException : Exception
{
    public EmptyValueException(string name) 
        : base($"Пустое значение поля {name}") { }
}
