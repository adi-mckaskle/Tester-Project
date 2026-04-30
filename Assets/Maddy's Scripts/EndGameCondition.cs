using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCondition : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    //[SerializeField] private string requiredItemName = "Name";
    [SerializeField] private string requiredItemName = "Reward";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Access the player's inventory to check for the item
            InventoryController inventoryController = other.GetComponent<InventoryController>();


        }
    }
}
