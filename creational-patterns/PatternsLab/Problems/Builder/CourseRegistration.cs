namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    private CourseRegistration(Builder builder)
    {
        if (string.IsNullOrWhiteSpace(builder.StudentEmail))
            throw new ArgumentException("Student email is required.", nameof(builder.StudentEmail));
        if (string.IsNullOrWhiteSpace(builder.CourseCode))
            throw new ArgumentException("Course code is required.", nameof(builder.CourseCode));
        if (string.IsNullOrWhiteSpace(builder.AccessMode))
            throw new InvalidOperationException("Access mode is required. Choose LiveGroup or VideosOnly.");

        if (builder.AccessMode == "LiveGroup" && string.IsNullOrWhiteSpace(builder.GroupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode.");
        if (builder.AccessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(builder.GroupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode.");

        StudentEmail = builder.StudentEmail;
        CourseCode = builder.CourseCode;
        AccessMode = builder.AccessMode;
        GroupCode = builder.GroupCode;
        DiscountCode = builder.DiscountCode;
        SendWhatsApp = builder.ShouldSendWhatsApp;
        SendEmailWelcome = builder.ShouldSendEmailWelcome;
        MentorNote = builder.MentorNote;
        PreferredStart = builder.PreferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";

    public sealed class Builder
    {
        internal string? StudentEmail { get; private set; }
        internal string? CourseCode { get; private set; }
        internal string? AccessMode { get; private set; }
        internal string? GroupCode { get; private set; }
        internal string? DiscountCode { get; private set; }
        internal bool ShouldSendWhatsApp { get; private set; }
        internal bool ShouldSendEmailWelcome { get; private set; }
        internal string? MentorNote { get; private set; }
        internal DateOnly? PreferredStart { get; private set; }

        public Builder ForStudent(string email)
        {
            StudentEmail = email;
            return this;
        }

        public Builder ForCourse(string courseCode)
        {
            CourseCode = courseCode;
            return this;
        }

        public Builder LiveGroup(string groupCode)
        {
            AccessMode = "LiveGroup";
            GroupCode = groupCode;
            return this;
        }

        public Builder VideosOnly()
        {
            AccessMode = "VideosOnly";
            return this;
        }

        public Builder WithGroupCode(string groupCode)
        {
            GroupCode = groupCode;
            return this;
        }

        public Builder WithDiscount(string discountCode)
        {
            DiscountCode = discountCode;
            return this;
        }

        public Builder SendWhatsApp()
        {
            ShouldSendWhatsApp = true;
            return this;
        }

        public Builder SendEmailWelcome()
        {
            ShouldSendEmailWelcome = true;
            return this;
        }

        public Builder WithMentorNote(string mentorNote)
        {
            MentorNote = mentorNote;
            return this;
        }

        public Builder StartingOn(DateOnly date)
        {
            PreferredStart = date;
            return this;
        }

        public CourseRegistration Build() => new(this);
    }
}

public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudentUgly()
    {
        return new CourseRegistration.Builder()
            .ForStudent("sara@mail.com")
            .ForCourse("SEF-101")
            .LiveGroup("G1")
            .WithDiscount("EARLY10")
            .SendWhatsApp()
            .SendEmailWelcome()
            .WithMentorNote("Needs evening slot")
            .StartingOn(new DateOnly(2026, 10, 1))
            .Build();
    }

    public static CourseRegistration CreateVideosOnlyUgly()
    {
        return new CourseRegistration.Builder()
            .ForStudent("ali@mail.com")
            .ForCourse("SEF-101")
            .VideosOnly()
            .SendEmailWelcome()
            .Build();
    }
}
