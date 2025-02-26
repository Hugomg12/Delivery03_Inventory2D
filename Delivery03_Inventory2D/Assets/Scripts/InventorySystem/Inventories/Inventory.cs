using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    // NOTE: One slot can contain multiple items of one type

    [SerializeField]
    List<ItemSlot> Slots;
    public int Length => Slots.Count;

    public Action OnInventoryChange;

    public bool ContainsItem(ItemBase item)
    {
        foreach (var slot in Slots)
        {
            if (slot.HasItem(item))
            {
                return true;
            }
        }
        return false;
    }
    public void AddItem(ItemBase item)
    {
        var slot = GetSlot(item);

        if (slot != null && item.IsStackable)
        {
            slot.AddOne();
        }
        else
        {
            slot = new ItemSlot(item);
            Slots.Add(slot);
        }
    }

    public void RemoveItem(ItemBase item)
    {
        var slot = GetSlot(item);

        if (slot != null)
        {
            slot.RemoveOne();
            if (slot.IsEmpty())
            {
                Slots.Remove(slot);
            }
        }
    }

    private void RemoveSlot(ItemSlot slot)
    {
        Slots.Remove(slot);
    }

    private ItemSlot GetSlot(ItemBase item)
    {
        foreach (var slot in Slots)
        {
            if (slot.HasItem(item))
            {
                return slot;
            }
        }
        return null;
    }

    public ItemSlot GetSlot(int i)
    {
        return Slots[i];
    }
}
