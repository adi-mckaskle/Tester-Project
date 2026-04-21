using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    // The player's score showing the total points earned
    public int score;

    // This method is automatically called by Unity
    // when the basket's collider enters the trigger collider of another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the "Good" tag
        // If so, add points and destroy the object
        if (other.CompareTag("Good"))
        {
            score += 10;
            Destroy(other.gameObject);
        }

        // Check if the object has the "Bad" tag
        // If so, subtract points and also destroy the object
        if (other.CompareTag("Bad"))
        {
            score -= 10;
            Destroy(other.gameObject);
        }
    }
}
