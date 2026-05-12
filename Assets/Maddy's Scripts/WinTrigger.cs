using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public int requiredItemId = 7;
    public GameObject winCanvas;

    private void OnTriggerEnter(Collider other)
    {
        //check if the thing that touched the box is the player
        if (other.CompareTag("Player"))
        {
            //asks inventory manager for the list of all items and their counts
            var counts = InventoryManager.Instance.GetItemCounts();

            //check if the inventory contains Key ID and if the amount is more than 0
            if (counts.ContainsKey(7) && counts[7] > 0)
            {
                // if they have item, call function to win game
                ActivateWinState();
            }
            else
            {
                //if they dont have it, just print a messgae (the player cant pass
                Debug.Log("Hey! You have soup to make!");
            }
        }
    }

    void ActivateWinState()
    {
        //turn on the Win Canvas
        winCanvas.SetActive(true);

        //Freeze game clock
        Time.timeScale = 0f;
        //unlock cursor
        //Cursor.lockState = CursorLockMode.None;
        ////make cursor visble
        //Cursor.visible = true;
    }
}
