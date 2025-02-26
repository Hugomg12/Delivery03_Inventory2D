using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Potion")]
public class ItemPotion : ConsumableItem
{
    public int HealthPoints;

    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}