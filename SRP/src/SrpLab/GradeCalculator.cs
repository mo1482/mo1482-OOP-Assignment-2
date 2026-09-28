namespace SrpLab;

public sealed class GradeCalculator
{
    public decimal Average(GradeBook book, string studentId)
    {
        if (!book.Scores.TryGetValue(studentId, out var list) || list.Count == 0)
            return 0m;

        return Math.Round(list.Average(), 2);
    }
}
