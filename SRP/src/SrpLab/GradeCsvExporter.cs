namespace SrpLab;

public sealed class GradeCsvExporter
{
    private readonly GradeCalculator _calculator;
    private readonly GradePolicy _policy;

    public GradeCsvExporter(GradeCalculator calculator, GradePolicy policy)
    {
        _calculator = calculator;
        _policy = policy;
    }

    public string Export(GradeBook book)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };

        foreach (var id in book.Scores.Keys.OrderBy(x => x))
        {
            var average = _calculator.Average(book, id);
            var letter = _policy.Letter(average);
            var honor = _policy.MeetsHonorRoll(average, letter);

            rows.Add($"{id},{average},{letter},{(honor ? 1 : 0)}");
        }

        return string.Join('
', rows);
    }
}
