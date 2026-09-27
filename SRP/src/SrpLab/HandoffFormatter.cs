namespace SrpLab;

public sealed class HandoffFormatter
{
    public string Format(int bed, string patient, int acuity, DateTime utcNow)
    {
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        return $"[HANDOFF {utcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}
