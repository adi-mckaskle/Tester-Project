using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{
    // How long the object stays in the scene
    public float aliveTimer = 5f;

    // Called once when the script starts
    void Start()
    {
        Destroy(gameObject, aliveTimer);
    }
}
