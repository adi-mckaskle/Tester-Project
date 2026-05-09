using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform inventoryPanel;

    private void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryUpdated += RefreshUI;
        }
    }
    private void OnDisable()
    {
        // 2. Unsubscribe to prevent "Ghost" errors
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryUpdated -= RefreshUI;
            RefreshUI();
        }
    }
    public void RefreshUI()
    {
        // The Clean Slate
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < InventoryManager.Instance.slots.Count; i++)
        {
            InventorySlot slotData = InventoryManager.Instance.slots[i];
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);

            // If the data says there's an item, draw it
            if (!slotData.IsEmpty)
            {
                DrawItemInSlot(slotData, newSlot.transform);
            }
        }
    }
    private void DrawItemInSlot(InventorySlot data, Transform slotTransform)
    {
        // 7. Get the visual prefab from the Dictionary
        GameObject prefab = ItemDictionary.Instance.GetItemPrefab(data.itemID);

        if (prefab != null)
        {
            GameObject visualItem = Instantiate(prefab, slotTransform);

            // 8. Update the item's internal quantity
            Item itemScript = visualItem.GetComponent<Item>();
            if (itemScript != null)
            {
                itemScript.quantity = data.quantity;
            }
        }
    }
}