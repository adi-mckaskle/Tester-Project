using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    // Right boundary of the spawn area
    public GameObject RightSide;

    // List of prefabs to spawn (object1, badobject)
    public GameObject[] items;

    // Delay before the first spawn and interval between the rest
    public float startDelay, repeatRate;

    void Start()
    {
        // Periodically call Spawn() at the specified interval
        InvokeRepeating("Spawn", startDelay, repeatRate);
    }

    void Spawn()
    {
        // Random position on X between the spawner and RightSide
        Vector3 pos = new Vector3(
            Random.Range(transform.position.x, RightSide.transform.position.x),
            transform.position.y,
            0
        );

        // Instantiate a random item from the list at the computed position
        Instantiate(items[Random.Range(0, items.Length)], pos, transform.rotation);
    }
}
