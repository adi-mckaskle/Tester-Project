using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventorySlotData
{
    public int itemID;
    public int quantity;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<InventorySlotData> playerItems = new();
    public int maxSlots = 24;

    public event UnityAction OnInventoryChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(int id, int qty)
    {
        // Stacking Logic: Check if we already have the item
        foreach (var slot in playerItems)
        {
            if (slot.itemID == id)
            {
                slot.quantity += qty;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        // New Slot Logic
        if (playerItems.Count < maxSlots)
        {
            playerItems.Add(new InventorySlotData { itemID = id, quantity = qty });
            OnInventoryChanged?.Invoke();
            return true;
        }

        Debug.Log("Inventory Full!");
        return false;
    }

    public void RemoveItem(int id, int qty)
    {
        for (int i = playerItems.Count - 1; i >= 0; i--)
        {
            if (playerItems[i].itemID == id)
            {
                playerItems[i].quantity -= qty;
                if (playerItems[i].quantity <= 0) playerItems.RemoveAt(i);
                break;
            }
        }
        OnInventoryChanged?.Invoke();
    }
}
