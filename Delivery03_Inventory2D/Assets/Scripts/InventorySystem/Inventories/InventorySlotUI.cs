using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image Image;
    public TextMeshProUGUI AmountText;

    private Canvas _canvas;
    private GraphicRaycaster _raycaster;
    private Transform _parent;
    private ItemBase _item;
    private InventoryUI _inventory;

    private Vector3 _originalPosition; // Guardar la posición original del ítem
    private bool isDragging = false;   // Indicar si el ítem se está arrastrando

    public InventoryItem InventoryItem { get; set; } // Referencia al InventoryItem asociado

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        Image.SetNativeSize();

        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = slot.Amount > 1;

        _item = slot.Item;
        _inventory = inventory;

        // Crear o asignar el InventoryItem asociado
        InventoryItem = GetComponent<InventoryItem>();
        if (InventoryItem == null)
        {
            InventoryItem = gameObject.AddComponent<InventoryItem>();
        }
        InventoryItem.Item = slot.Item; // Asignar el ítem al InventoryItem
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Solo seleccionar el ítem si no se está arrastrando
        if (!isDragging)
        {
            InventoryManager.Instance.SelectItem(InventoryItem);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true; // Indicar que el ítem se está arrastrando

        _parent = transform.parent;
        _originalPosition = transform.localPosition;

        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);

        if (!_canvas)
        {
            _canvas = GetComponentInParent<Canvas>();
            _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        }

        transform.SetParent(_canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false; // Indicar que el ítem ya no se está arrastrando

        var results = new List<RaycastResult>();
        _raycaster.Raycast(eventData, results);

        bool itemMoved = false;

        foreach (var result in results)
        {
            var targetInventoryUI = result.gameObject.GetComponent<InventoryUI>();
            if (targetInventoryUI != null && targetInventoryUI != _inventory)
            {
                MoveItemBetweenInventories(_inventory.Inventory, targetInventoryUI.Inventory);
                itemMoved = true;
                break;
            }
        }

        if (!itemMoved)
        {
            Debug.Log("Item dropped outside inventory. Returning to original position.");
            ReturnToOriginalPosition();
        }

        transform.SetParent(_parent.transform);
        transform.localPosition = _originalPosition;
    }

    private void ReturnToOriginalPosition()
    {
        // Restaurar la posición original del ítem
        transform.localPosition = _originalPosition;
    }

    private void MoveItemBetweenInventories(Inventory sourceInventory, Inventory targetInventory)
    {
        if (sourceInventory == null || targetInventory == null || _item == null)
        {
            Debug.LogError("Invalid inventories or item!");
            return;
        }

        // Verificar si el ítem se está moviendo de la tienda al jugador (compra)
        if (sourceInventory == InventoryManager.Instance.shopInventory && targetInventory == InventoryManager.Instance.playerInventory)
        {
            int itemCost = _item.Cost;

            if (InventoryManager.Instance.playerCoins >= itemCost)
            {
                // Restar el costo del ítem de las monedas del jugador
                InventoryManager.Instance.playerCoins -= itemCost;

                // Mover el ítem de la tienda al jugador
                sourceInventory.RemoveItem(_item);
                targetInventory.AddItem(_item);

                // Actualizar la UI
                InventoryManager.Instance.UpdateUI();
                InventoryManager.Instance.UpdateCoinsUI();

                Debug.Log("Item bought! Remaining coins: " + InventoryManager.Instance.playerCoins);
            }
            else
            {
                Debug.Log("Not enough coins to buy this item!");
            }
        }
        // Verificar si el ítem se está moviendo del jugador a la tienda (venta)
        else if (sourceInventory == InventoryManager.Instance.playerInventory && targetInventory == InventoryManager.Instance.shopInventory)
        {
            int itemCost = _item.Cost;

            // Sumar el valor del ítem a las monedas del jugador
            InventoryManager.Instance.playerCoins += itemCost;

            // Mover el ítem del jugador a la tienda
            sourceInventory.RemoveItem(_item);
            targetInventory.AddItem(_item);

            // Actualizar la UI
            InventoryManager.Instance.UpdateUI();
            InventoryManager.Instance.UpdateCoinsUI();

            Debug.Log("Item sold! Current coins: " + InventoryManager.Instance.playerCoins);
        }
        // Si el ítem se mueve dentro del mismo inventario, no hacer nada
        else
        {
            Debug.Log("Item moved within the same inventory.");
        }
    }
}