using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public string requiredQuestID = "Final_Quest";
    public GameObject winUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            // 2. Check if the QuestController says the quest is handed in
            if (QuestController.Instance.IsQuestHandedIn("Final_Quest"))
            {
                ActivateWin();
            }
            else
            {
                Debug.Log("The exit is locked! Finish the quest: " + requiredQuestID);
            }
        }
    }

    void ActivateWin()
    {
        if (winUI != null)
        {
            winUI.SetActive(true);
            Time.timeScale = 0f; // Freezes the game world
            Debug.Log("Victory!");
        }
        else
        {
            Debug.LogError("WinTrigger: No Win UI assigned in the Inspector!");
        }
    }
}