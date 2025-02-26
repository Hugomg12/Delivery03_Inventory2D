// SOLID: Interfaces

// Interface for items that can be picked
public interface ICanBePicked
{
    void PickedUp();
    ItemBase GetItem();
}

// Interface for pickable item, implements PickUp()
public interface IPickUp
{
    void PickUp(ICanBePicked item);
}

// Interface for consumable item, implements Use()
public interface IConsume
{
    void Use(ConsumableItem item);
}