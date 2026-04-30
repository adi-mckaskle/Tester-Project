using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName ="NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public bool[] endDialogueLines; //Mark where dialogue ends
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip voicesound;
    public float voicePitch = 1f;

    public DialogueChoice[] choices;
   
    public int questInProgressIndex; //What they say when quest is in progress
    public int questCompletedIndex; //What they say when quest is completed
    public Quest quest; //Quest NPC gives
}

[System.Serializable]

public class DialogueChoice
{
    public int dialogueIndex; //Dialogue line where choices appear
    public string[] choices; //Player response options
    public int[] nextDialogueIndexes; //Where choice leads
    public bool[] givesQuest; 
}
