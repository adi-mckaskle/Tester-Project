using UnityEngine;
using UnityEngine.SceneManagement; //1

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // 1. Tell the Managers to wipe their data
        if (QuestController.Instance != null)
            QuestController.Instance.ResetQuests();

        if (InventoryManager.Instance != null)
            InventoryManager.Instance.ResetInventory();

        // 2. Reset time scale in case the Win Screen paused it
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
