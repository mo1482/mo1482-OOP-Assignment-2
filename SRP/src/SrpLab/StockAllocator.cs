namespace SrpLab;

public sealed class StockAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(WarehousePickList list)
    {
        var result = new List<(string, int)>();

        foreach (var line in list.Lines)
            result.Add((line.Sku, Math.Min(line.QtyNeeded, line.QtyOnHand)));

        return result;
    }
}
