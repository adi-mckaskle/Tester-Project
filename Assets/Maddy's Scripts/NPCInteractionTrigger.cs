using UnityEngine;

public class NPCInteractionTrigger : MonoBehaviour
{

    public GameObject interactionMarker; //drag sprite here
    public KeyCode interactKey = KeyCode.E;

    public NPC npcScript;

    public bool isPlayerInRange = false;

    private void Start()
    {;
        if (interactionMarker != null)
        {
            interactionMarker.SetActive(false);
        }

        if (npcScript == null)
        {
            npcScript = GetComponent<NPC>();
        }
    }
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            //trigger  the start dialogue in npc script
            if (npcScript != null)
            {
                npcScript.StartDialogue();

                //hidemarker while talking
                interactionMarker.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionMarker != null)
            {
                interactionMarker.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionMarker != null)
            {
                interactionMarker.SetActive(false);
            }
            if (npcScript != null)
            {
                npcScript.EndDialogue();
            }
        }
    }

}
