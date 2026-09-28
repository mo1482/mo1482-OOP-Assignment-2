namespace SrpLab;

public sealed class PickerScriptFormatter
{
    private readonly StockAllocator _allocator;
    private readonly WalkingOrderPlanner _planner;

    public PickerScriptFormatter(StockAllocator allocator, WalkingOrderPlanner planner)
    {
        _allocator = allocator;
        _planner = planner;
    }

    public string Format(WarehousePickList list)
    {
        var steps = _planner.Plan(list)
            .Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");

        var allocations = _allocator.Allocate(list);
        var shortfallSkus = new List<string>();

        foreach (var allocation in allocations)
        {
            var line = list.Lines.First(l => l.Sku == allocation.Sku);
            if (allocation.Allocated < line.QtyNeeded)
                shortfallSkus.Add(allocation.Sku);
        }

        var warn = shortfallSkus.Count > 0
            ? "SHORTAGES: " + string.Join(", ", shortfallSkus)
            : "SHORTAGES: none";

        return string.Join('
', steps) + "
" + warn;
    }
}
