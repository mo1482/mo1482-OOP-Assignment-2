namespace SrpLab;

public sealed class AppointmentSchedule
{
    private readonly HashSet<DateTimeOffset> _booked = new();

    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public AppointmentSchedule(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }

    public bool IsBooked(DateTimeOffset slot) => _booked.Contains(slot);
    public void Book(DateTimeOffset slot) => _booked.Add(slot);
}

public sealed class BusinessHoursPolicy
{
    public bool IsWithin(AppointmentSchedule schedule, DateTimeOffset when)
    {
        if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday) return false;
        var t = TimeOnly.FromDateTime(when.DateTime);
        return t >= schedule.Open && t.AddMinutes(schedule.SlotMinutes) <= schedule.Close;
    }
}

public sealed class AppointmentFinder
{
    private readonly BusinessHoursPolicy _hours = new();

    public DateTimeOffset? FindNext(AppointmentSchedule schedule, DateTimeOffset from, int searchHours)
    {
        var cursor = Align(schedule, from);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (_hours.IsWithin(schedule, cursor) && !schedule.IsBooked(cursor))
                return cursor;

            cursor = cursor.AddMinutes(schedule.SlotMinutes);
        }

        return null;
    }

    private DateTimeOffset Align(AppointmentSchedule schedule, DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % schedule.SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}

public sealed class AppointmentBooker
{
    private readonly BusinessHoursPolicy _hours = new();

    public bool TryBook(AppointmentSchedule schedule, DateTimeOffset slot)
    {
        if (!_hours.IsWithin(schedule, slot) || schedule.IsBooked(slot)) return false;
        schedule.Book(slot);
        return true;
    }
}

public sealed class IcsFormatter
{
    public string Format(AppointmentSchedule schedule, DateTimeOffset slot, string patientName, string clinician)
    {
        var uid = Guid.NewGuid();
        var end = slot.AddMinutes(schedule.SlotMinutes);

        return "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\n" +
               $"UID:{uid}\nDTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}\nDTEND:{end:yyyyMMdd'T'HHmmss'Z'}\n" +
               $"SUMMARY:Visit {patientName} / {clinician}\nEND:VEVENT\nEND:VCALENDAR\n";
    }
}

public sealed class SmsReminderFormatter
{
    public string Format(DateTimeOffset slot, string clinicPhone)
        => $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
}
