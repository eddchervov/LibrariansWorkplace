namespace LibrariansWorkplace.Domain.Interfaces;

public interface ISystemClock
{
    public DateTime UtcNow { get; }
    public DateTime UtcToday { get; }
}
