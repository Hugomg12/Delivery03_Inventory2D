using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory Inventory;
    public InventorySlotUI SlotPrefab;

    List<GameObject> _shownObjects;

    void Start()
    {
        FillInventory(Inventory);
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChange += UpdateInventory;
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= UpdateInventory;
    }

    public void UpdateInventoryUI()
    {
        ClearInventory();

        FillInventory(Inventory);
    }

    private void UpdateInventory()
    {
        // Regenerate full inventory on changes
        ClearInventory();
        FillInventory(Inventory);
    }

    private void ClearInventory()
    {
        if (_shownObjects == null) return;
        foreach (var item in _shownObjects)
        {
            if (item) Destroy(item);
        }

        _shownObjects.Clear();
    }

    private void FillInventory(Inventory inventory)
    {
        // Lazy initialization for objects list
        if (_shownObjects == null) _shownObjects = new List<GameObject>();

        if (_shownObjects.Count > 0) ClearInventory();

        for (int i = 0; i < inventory.Length; i++)
        {
            _shownObjects.Add(AddSlot(inventory.GetSlot(i)));
        }
    }

    private GameObject AddSlot(ItemSlot inventorySlot)
    {
        // Add a new visual slot UI in inventory UI, using provided prefab
        var element = GameObject.Instantiate(SlotPrefab, Vector3.zero, Quaternion.identity, transform);

        element.Initialize(inventorySlot, this);

        return element.gameObject;
    }

    public void UseItem(ItemBase item)
    {
        Inventory.RemoveItem(item);
    }
}
