using UnityEngine;
using UnityEngine.SceneManagement; //1

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        //if (InventoryManager.Instance != null) InventoryManager.Instance.ResetInventory();
        // If you have a ResetQuests() in your QuestController, call it here too.
        SceneManager.LoadScene(1);
    }
}
