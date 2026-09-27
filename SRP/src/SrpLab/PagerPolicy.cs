namespace SrpLab;

public sealed class PagerPolicy
{
    public bool RequiresYellowCode(int acuity) => acuity >= 8;

    public string BuildMessage(int bed, DateTime utcNow) =>
        $"CODE-YELLOW bed={bed} at {utcNow:HH:mm}";
}
