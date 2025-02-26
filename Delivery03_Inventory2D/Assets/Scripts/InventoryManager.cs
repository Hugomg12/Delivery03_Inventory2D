using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private InventoryItem selectedItem;

    //public List<InventoryItem> playerInventory = new List<InventoryItem>();
    //public List<InventoryItem> shopInventory = new List<InventoryItem>();

    public Inventory playerInventory; // Inventario del jugador (ScriptableObject)
    public Inventory shopInventory;   // Inventario de la tienda (ScriptableObject)

    public InventoryUI playerInventoryUI;
    public InventoryUI shopInventoryUI;

    public int playerCoins = 100;
    public TextMeshProUGUI coinsText;
    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void SelectItem(InventoryItem newItem)
    {
        if (selectedItem != null)
        {
            selectedItem.SelectItem(false); // Desseleccionar el anterior
        }

        selectedItem = newItem;
        selectedItem.SelectItem(true); // Seleccionar el nuevo
    }

    public void BuyItem()
    {
        if (selectedItem != null && shopInventory.ContainsItem(selectedItem.Item))
        {
            int itemCost = selectedItem.Item.Cost;

            if (playerCoins >= itemCost)
            {
                // Restar el costo del ítem de las monedas del jugador
                playerCoins -= itemCost;

                // Restar una unidad del ítem en el inventario de la tienda
                shopInventory.RemoveItem(selectedItem.Item);

                // Añadir el ítem al inventario del jugador
                playerInventory.AddItem(selectedItem.Item);

                // Deseleccionar el ítem y actualizar la UI
                selectedItem.SelectItem(false);
                selectedItem = null;
                UpdateUI();
                UpdateCoinsUI();


                Debug.Log("Item bought! Remaining coins: " + playerCoins);
            }
            else
            {
                Debug.Log("Not enough coins to buy this item!");
            }
        }
    }

    public void SellItem()
    {
        if (selectedItem != null && playerInventory.ContainsItem(selectedItem.Item))
        {
            // Sumar el valor del ítem a las monedas del jugador
            playerCoins += selectedItem.Item.Cost;

            // Eliminar el ítem del inventario del jugador
            playerInventory.RemoveItem(selectedItem.Item);

            // Añadir el ítem al inventario de la tienda
            shopInventory.AddItem(selectedItem.Item);

            // Deseleccionar el ítem y actualizar la UI
            selectedItem.SelectItem(false);
            selectedItem = null;
            UpdateUI();
            UpdateCoinsUI();

            Debug.Log("Item sold! Current coins: " + playerCoins);
        }
    }

    public void UpdateUI()
    {
        // Actualizar la UI del inventario del jugador
        if (playerInventoryUI != null)
        {
            playerInventoryUI.UpdateInventoryUI();
        }

        // Actualizar la UI del inventario de la tienda
        if (shopInventoryUI != null)
        {
            shopInventoryUI.UpdateInventoryUI();
        }


    }

    public void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + playerCoins;
        }
        // Aquí actualizarías la interfaz gráfica según cómo estén implementados los inventarios en tu juego.
    }
}
