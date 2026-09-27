namespace SrpLab;

public sealed class WmsXmlFormatter
{
    private readonly StockAllocator _allocator;

    public WmsXmlFormatter(StockAllocator allocator) => _allocator = allocator;

    public string Format(WarehousePickList list, string batchId)
    {
        var parts = _allocator.Allocate(list)
            .Select(a => $"<line sku="{a.Sku}" qty="{a.Allocated}" />");

        return $"<batch id="{batchId}">{string.Join("", parts)}</batch>";
    }
}
