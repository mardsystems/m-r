using Inventory.Data;

namespace Inventory.Modules.Items;

public class ReadModelFacade
{
    public IEnumerable<InventoryItemListDto> GetInventoryItems()
    {
        return FakeDatabase.list;
    }

    public InventoryItemDetailsDto GetInventoryItemDetails(Guid id)
    {
        return FakeDatabase.details[id];
    }
}
