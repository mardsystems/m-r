using System;

namespace Inventory.Modules.Items;

public class InventoryItem : AggregateRoot
{
    private Guid _id;
    
    private bool _activated;

    public int AvailableQty { get; private set; }

    public int MaxQty { get; private set; } = 5;

    private void Apply(InventoryItemCreated e)
    {
        _id = e.Id;
        _activated = true;
    }

    private void Apply(InventoryItemDeactivated e)
    {
        _activated = false;
    }

    private void Apply(MaxQtyChanged e)
    {
        MaxQty = e.NewMaxQty;
    }

    private void Apply(ItemsCheckedInToInventory e)
    {
        AvailableQty += e.Count;
    }

    private void Apply(ItemsRemovedFromInventory e)
    {
        AvailableQty -= e.Count;
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName)) throw new ArgumentException("newName");

        ApplyChange(new InventoryItemRenamed(_id, newName));
    }

    public void Remove(int count)
    {
        if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");
        
        if (AvailableQty - count < 0) throw new InvalidOperationException("Cannot remove a count greater than the available quantity");
        
        ApplyChange(new ItemsRemovedFromInventory(_id, count));
    }

    public void CheckIn(int count)
    {
        if (count <= 0) throw new InvalidOperationException("must have a count greater than 0 to add to inventory");
        
        if (AvailableQty + count > MaxQty) throw new InvalidOperationException("Checked in count will exceed Max Qty");
        
        ApplyChange(new ItemsCheckedInToInventory(_id, count));
    }

    public void ChangeMaxQty(int newMaxQty)
    {
        if (newMaxQty <= 0) throw new InvalidOperationException("New Max Qty must be larger than 0");
        
        if (newMaxQty < AvailableQty) throw new InvalidOperationException("New Max Qty cannot be less than Available Qty");
        
        ApplyChange(new MaxQtyChanged(_id, newMaxQty));
    }

    public void Deactivate()
    {
        if (!_activated) throw new InvalidOperationException("already deactivated");
        
        ApplyChange(new InventoryItemDeactivated(_id));
    }

    public override Guid Id
    {
        get { return _id; }
    }

    public InventoryItem()
    {
        // used to create in repository ... many ways to avoid this, eg making private constructor
    }

    public InventoryItem(Guid id, string name)
    {
        ApplyChange(new InventoryItemCreated(id, name, MaxQty));
    }
}
