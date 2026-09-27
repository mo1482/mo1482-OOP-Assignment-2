namespace SrpLab;

public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();

    public void AssignBed(int bed, string patientId, int acuity)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = acuity;
    }

    public bool TryGetBed(int bed, out string patient, out int acuity)
    {
        if (_bedPatient.TryGetValue(bed, out patient!))
        {
            acuity = _vitalsScore[bed];
            return true;
        }

        acuity = 0;
        patient = string.Empty;
        return false;
    }

    public IReadOnlyDictionary<int, string> Patients => _bedPatient;
    public IReadOnlyDictionary<int, int> AcuityScores => _vitalsScore;
}
