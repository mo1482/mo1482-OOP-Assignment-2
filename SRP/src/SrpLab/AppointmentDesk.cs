namespace SrpLab;

public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();

    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }

    public bool IsBooked(DateTimeOffset slot) => _booked.Contains(slot);

    public void Book(DateTimeOffset slot)
    {
        if (!IsWithinBusinessHours(slot) || _booked.Contains(slot))
            throw new InvalidOperationException("Invalid or already booked slot.");

        _booked.Add(slot);
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday) return false;

        var t = TimeOnly.FromDateTime(when.DateTime);
        return t >= Open && t.AddMinutes(SlotMinutes) <= Close;
    }
}
