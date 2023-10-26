namespace Inventory.Modules.Items;

public class InventoryItemDetailsDto
{
    public Guid Id;
    public string Name;
    public int MaxQty;
    public int CurrentCount;
    public int Version;

    public InventoryItemDetailsDto(Guid id, string name, int maxQty, int currentCount, int version)
    {
        Id = id;
        Name = name;
        MaxQty = maxQty;
        CurrentCount = currentCount;
        Version = version;
    }
}

public class InventoryItemListDto
{
    public Guid Id;
    public string Name;

    public InventoryItemListDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
