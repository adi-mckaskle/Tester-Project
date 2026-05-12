using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1f;

        //if (InventoryManager.Instance != null) // 5
        //{
        //    InventoryManager.Instance.ResetInventory(); // 6
        //}
        //if (QuestController.Instance != null)
        //{
        //    QuestController.Instance.ResetQuest();
        //}

        SceneManager.LoadScene(1);
    }
}
