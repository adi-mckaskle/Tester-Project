using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventorySlot
{
    public int itemID = -1;
    public int quantity = 0;

    public bool IsEmpty => itemID == -1;
}


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public int maxSlots = 24; //How many boxes in the grid
    public List<InventorySlot> slots = new List<InventorySlot>();

    public event UnityAction OnInventoryUpdated;

    private void Awake()
    {
        // 4. THE PERSISTENCE LOGIC (Level Loop Fix)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInventory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetInventory()
    {
        InitializeInventory(); // Resets all slots to -1 itemID
        OnInventoryUpdated?.Invoke();
        Debug.Log("InventoryManager: All items cleared.");
    }

    private void InitializeInventory()
    {
        slots.Clear();
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(int id, int amount)
    {
        // We skip the "Find existing ID" loop entirely.
        // Instead, we just look for the first empty slot.
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.itemID = id;
                slot.quantity = amount;

                OnInventoryUpdated?.Invoke();
                return true;
            }
        }

        Debug.Log("Inventory is full! No empty boxes available.");
        return false;
    }

    public Dictionary<int, int> GetItemCounts()
    {
        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach (var slot in slots)
        {
            if (slot.IsEmpty) continue;

            if (counts.ContainsKey(slot.itemID))
            {
                counts[slot.itemID] += slot.quantity;
            }
            else
            {
                counts[slot.itemID] = slot.quantity;
            }
        }
        return counts;
    }

    public void RemoveItemsFromInventory(int id, int amountToRemove)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (amountToRemove <= 0) break;

            if (slots[i].itemID == id)
            {
                int canRemove = Mathf.Min(slots[i].quantity, amountToRemove);
                slots[i].quantity -= canRemove;
                amountToRemove -= canRemove;

                if (slots[i].quantity <= 0) slots[i].itemID = -1;
            }
        }
        OnInventoryUpdated?.Invoke();
    }

    public int GetTotalQuantity(int id)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            if (slot.itemID == id) total += slot.quantity;
        }
        return total;
    }
}

