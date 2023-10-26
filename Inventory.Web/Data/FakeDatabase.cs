using Inventory.Modules.Items;

namespace Inventory.Data;

internal class FakeDatabase
{
    public static Dictionary<Guid, InventoryItemDetailsDto> details = new Dictionary<Guid, InventoryItemDetailsDto>();

    public static List<InventoryItemListDto> list = new List<InventoryItemListDto>();
}
