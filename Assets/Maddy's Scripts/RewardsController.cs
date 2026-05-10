using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    public static RewardsController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GiveQuestReward(Quest quest)
    {
        if (quest?.questRewards == null) return;

        foreach (var reward in quest.questRewards)
        {
            switch (reward.type)
            {
                case RewardType.Item:
                    GiveItemReward(reward.rewardID, reward.amount);
                    break;
                case RewardType.Gold:
                    break;
                case RewardType.Experience:
                    break;
                case RewardType.Custom:
                    break;
            }
        }
    }

    public void GiveItemReward(int itemID, int amount)
    {
        bool added = InventoryManager.Instance.AddItem(itemID, amount);

        if (!added)
        {
            GameObject itemPrefab = ItemDictionary.Instance.GetItemPrefab(itemID);
            if (itemPrefab != null)
            {
                GameObject dropItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
                dropItem.GetComponent<BounceEffect>()?.StartBounce();

                if (dropItem.TryGetComponent(out Item itemScript))
                {
                    itemScript.quantity = amount;
                }

            }
        }
        else
        {
            GameObject itemPrefab = ItemDictionary.Instance.GetItemPrefab(itemID);
            itemPrefab?.GetComponent<Item>()?.ShowPopUp();
        }
    }
}
