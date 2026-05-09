using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                //Add item inventory
                bool itemAdded = InventoryManager.Instance.AddItem(item.ID, item.quantity);

                if (itemAdded)
                {
                    item.ShowPopUp();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
