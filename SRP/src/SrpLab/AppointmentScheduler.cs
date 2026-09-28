namespace SrpLab;

public sealed class AppointmentScheduler
{
    private readonly AppointmentDesk _desk;

    public AppointmentScheduler(AppointmentDesk desk) => _desk = desk;

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (_desk.IsWithinBusinessHours(cursor) && !_desk.IsBooked(cursor))
                return cursor;

            cursor = cursor.AddMinutes(_desk.SlotMinutes);
        }

        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!_desk.IsWithinBusinessHours(slot) || _desk.IsBooked(slot))
            return false;

        _desk.Book(slot);
        return true;
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % _desk.SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}
