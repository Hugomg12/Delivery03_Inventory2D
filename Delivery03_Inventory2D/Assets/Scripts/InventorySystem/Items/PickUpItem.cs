using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, ICanBePicked
{
    public ItemBase Item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get PlayerInventory component on Player gameObject
        var picker = other.GetComponent<IPickUp>();

        if (picker != null)
        {
            picker.PickUp(this); // Add to player inventory

            PickedUp(); // Destroy game object from screen
        }
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }

    public ItemBase GetItem()
    {
        return Item;
    }
}
