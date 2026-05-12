using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    public List<QuestProgress> activateQuests = new();
    private QuestUI questUI;

    public List<string> handinQuestIDs = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        questUI = FindAnyObjectByType<QuestUI>();

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryUpdated += CheckInventoryForQuests;
        }
    }

    public void AcceptQuest(Quest quest)
    {
        if (IsQuestActive(quest.questID)) return;

        activateQuests.Add(new QuestProgress(quest));

        CheckInventoryForQuests();

        //safety check in case UI is hidden
        if (questUI == null) questUI = FindAnyObjectByType<QuestUI>();
        questUI?.UpdateQuestUI();
    }

    public bool IsQuestActive(string questID) => activateQuests.Exists(q => q.QuestID == questID);

    public void CheckInventoryForQuests()
    {
        Dictionary<int, int> itemCounts = InventoryManager.Instance.GetItemCounts();

        foreach (QuestProgress quest in activateQuests)
        {
            foreach (QuestObjective questObjective in quest.objectives)
            {
                //only check 'CollectItem' objectives
                if (questObjective.type != ObjectiveType.CollectItem) continue;

                //convert the string id from inspector to a number
                if (!int.TryParse(questObjective.objectiveID, out int itemID))
                {
                    Debug.LogWarning($"Quest {quest.QuestID} failed! The ID is currently: [{questObjective.objectiveID}]");
                    continue;
                }
                
                int newAmount = itemCounts.TryGetValue(itemID, out int count) ? Mathf.Min(count, questObjective.requiredAmount) : 0;

                if (questObjective.currentAmount != newAmount)
                {
                    questObjective.currentAmount = newAmount;
                }
            }
        }
        
        if (questUI == null) questUI = FindAnyObjectByType<QuestUI>();
        questUI?.UpdateQuestUI();
    }

    public bool IsQuestCompleted(string questID)
    {
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        return quest != null && quest.objectives.TrueForAll(o => o.IsCompleted);
    }

    public void HandInQuest(string questID)
    {
        //Try remove required items
        if (!RemoveRequiredItemsFromInventory(questID))
        {
            //Quest couldn't be completed - missing items
            return;
        }

        //Remove quest from quest log
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        if (quest != null)
        {
            handinQuestIDs.Add(questID);
            activateQuests.Remove(quest);

            if (questUI == null) questUI = FindAnyObjectByType<QuestUI>();
            questUI?.UpdateQuestUI();
        }
    }
    public bool IsQuestHandedIn(string questID)
    {
        return handinQuestIDs.Contains(questID);
    }

    public bool RemoveRequiredItemsFromInventory(string questID)
    {
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        if (quest == null) return false;

        Dictionary<int, int> requiredItems = new();

        //Item requirements from objectives
        foreach (QuestObjective objective in quest.objectives)
        {
            if (objective.type == ObjectiveType.CollectItem && int.TryParse(objective.objectiveID, out int itemID))
            {
                requiredItems[itemID] = objective.requiredAmount;
            }
        }

        //Verify we have items
        Dictionary<int, int> itemCounts = InventoryManager.Instance.GetItemCounts();
        foreach (var item in requiredItems)
        {
            if (itemCounts.GetValueOrDefault(item.Key) < item.Value)
            {
                //Not enough items to complete quest
                return false;
            }
        }

        //Remove required items from inventory
        foreach (var itemRequirement in requiredItems)
        {
            InventoryManager.Instance.RemoveItemsFromInventory(itemRequirement.Key, itemRequirement.Value);
        }

        return true;
    }
}