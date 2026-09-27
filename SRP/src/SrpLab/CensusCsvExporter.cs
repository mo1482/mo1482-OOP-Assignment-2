namespace SrpLab;

public sealed class CensusCsvExporter
{
    public string Export(WardBoard board)
    {
        var lines = new List<string> { "bed,patient,acuity" };

        foreach (var bed in board.Patients.Keys.OrderBy(x => x))
            lines.Add($"{bed},{board.Patients[bed]},{board.AcuityScores[bed]}");

        return string.Join('\n', lines);
    }
}
