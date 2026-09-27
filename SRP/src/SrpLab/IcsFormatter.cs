namespace SrpLab;

public sealed class IcsFormatter
{
    public string Format(DateTimeOffset slot, int slotMinutes, string patientName, string clinician)
    {
        var uid = Guid.NewGuid();
        var end = slot.AddMinutes(slotMinutes);

        return "BEGIN:VCALENDAR
VERSION:2.0
BEGIN:VEVENT
" +
               $"UID:{uid}
DTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}
DTEND:{end:yyyyMMdd'T'HHmmss'Z'}
" +
               $"SUMMARY:Visit {patientName} / {clinician}
END:VEVENT
END:VCALENDAR
";
    }
}
