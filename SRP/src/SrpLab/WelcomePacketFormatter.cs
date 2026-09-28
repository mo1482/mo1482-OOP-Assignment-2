namespace SrpLab;

public sealed class WelcomePacketFormatter
{
    public string Format(CourseEnrollmentDesk course, string studentEmail, string studentName)
    {
        var status = course.IsSeated(studentEmail)
            ? "confirmed seat"
            : $"waitlist #{course.WaitlistPosition(studentEmail)}";

        return $"# Welcome to {course.CourseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{course.CourseCode.ToLowerInvariant()}\n";
    }
}
