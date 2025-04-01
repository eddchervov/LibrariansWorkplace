using LibrariansWorkplace.Domain.Interfaces;

namespace LibrariansWorkplace.Infrastructure.BL;

public class SystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime UtcToday => DateTime.UtcNow.Date;
}
