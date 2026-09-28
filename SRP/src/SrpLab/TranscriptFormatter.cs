namespace SrpLab;

public sealed class TranscriptFormatter
{
    private readonly GradeCalculator _calculator;
    private readonly GradePolicy _policy;

    public TranscriptFormatter(GradeCalculator calculator, GradePolicy policy)
    {
        _calculator = calculator;
        _policy = policy;
    }

    public string Plain(GradeBook book, string studentId, string fullName)
    {
        var average = _calculator.Average(book, studentId);
        var letter = _policy.Letter(average);
        var honor = _policy.MeetsHonorRoll(average, letter);

        return $"TRANSCRIPT
Student: {fullName} ({studentId})
Average: {average}
Letter: {letter}
Honor: {honor}
";
    }
}
