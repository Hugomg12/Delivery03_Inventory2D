using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public Image selectionHighlight; // Borde o efecto de selección
    public Color selectedColor = Color.red;   // Color cuando está seleccionado
    public Color deselectedColor = Color.white; // Color cuando no está seleccionado

    private bool isSelected = false;

    public ItemBase Item { get;  set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.SelectItem(this);
    }

    public void SelectItem(bool select)
    {
        isSelected = select; // Asignar directamente el estado

        // Cambiar el color según el estado de selección
        selectionHighlight.color = isSelected ? selectedColor : deselectedColor;
    }
}
