using Inventory.Bus;
using Inventory.Data;

namespace Inventory.Modules.Items;

public class InventoryListView : Handles<InventoryItemCreated>, Handles<InventoryItemRenamed>, Handles<InventoryItemDeactivated>
{
    public void Handle(InventoryItemCreated message)
    {
        FakeDatabase.list.Add(new InventoryItemListDto(message.Id, message.Name));
    }

    public void Handle(InventoryItemRenamed message)
    {
        var item = FakeDatabase.list.Find(x => x.Id == message.Id);
        item.Name = message.NewName;
    }

    public void Handle(InventoryItemDeactivated message)
    {
        FakeDatabase.list.RemoveAll(x => x.Id == message.Id);
    }
}

public class InventoryItemDetailView : Handles<InventoryItemCreated>, Handles<InventoryItemDeactivated>, Handles<InventoryItemRenamed>, Handles<ItemsRemovedFromInventory>, Handles<ItemsCheckedInToInventory>, Handles<MaxQtyChanged>
{
    public void Handle(InventoryItemCreated message)
    {
        FakeDatabase.details.Add(message.Id, new InventoryItemDetailsDto(message.Id, message.Name, message.MaxQty, 0, 0));
    }

    public void Handle(InventoryItemRenamed message)
    {
        InventoryItemDetailsDto d = GetDetailsItem(message.Id);
        d.Name = message.NewName;
        d.Version = message.Version;
    }

    private InventoryItemDetailsDto GetDetailsItem(Guid id)
    {
        InventoryItemDetailsDto d;

        if (!FakeDatabase.details.TryGetValue(id, out d))
        {
            throw new InvalidOperationException("did not find the original inventory this shouldnt happen");
        }

        return d;
    }

    public void Handle(ItemsRemovedFromInventory message)
    {
        InventoryItemDetailsDto d = GetDetailsItem(message.Id);
        d.CurrentCount -= message.Count;
        d.Version = message.Version;
    }

    public void Handle(ItemsCheckedInToInventory message)
    {
        InventoryItemDetailsDto d = GetDetailsItem(message.Id);
        d.CurrentCount += message.Count;
        d.Version = message.Version;
    }

    public void Handle(InventoryItemDeactivated message)
    {
        FakeDatabase.details.Remove(message.Id);
    }

    public void Handle(MaxQtyChanged message)
    {
        InventoryItemDetailsDto d = GetDetailsItem(message.Id);
        d.MaxQty = message.NewMaxQty;
        d.Version = message.Version;
    }
}
