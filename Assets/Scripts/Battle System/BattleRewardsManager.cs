using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleRewardsManager : MonoBehaviour
{

    public static BattleRewardsManager instance;

    [SerializeField] TextMeshProUGUI XpText, itemsText;
    [SerializeField] GameObject rewardScreen;

    [SerializeField] ItemsManager[] rewardItems;
    [SerializeField] int xpReward;

    [SerializeField] bool markQuestComplete;
    [SerializeField] string questToMarkName;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OpenRewardScreen(15000, rewardItems);
        }
    }

    public void OpenRewardScreen(int xpEarned, ItemsManager[] itemsEarned)
    {
        xpReward = xpEarned;
        rewardItems = itemsEarned;
        XpText.text = xpEarned + " XP";
        itemsText.text = "";
        
        foreach(ItemsManager rewardItemText in rewardItems)
        {
            itemsText.text += rewardItemText.itemName + ", ";
        }
        itemsText.text = itemsText.text.Remove(itemsText.text.Length - 1);
        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        foreach(PlayerStats activePlayer in GameManager.instance.GetPlayerStats())
        {
            if (activePlayer.gameObject.activeInHierarchy)
            {
                activePlayer.AddXP(xpReward);

            }
        }
        foreach(ItemsManager itemRewarded in rewardItems)
        {
            Inventory.instance.AddItems(itemRewarded);
        }
        rewardScreen.SetActive(false);
        GameManager.instance.battleIsActive = false;

        if (markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMarkName);
        }
    }

    public void SetMarkQuestComplete(bool quest)
    {
        markQuestComplete = quest;
    }
    public void SetQuestToMarkName(string questName)
    {
        questToMarkName = questName;
    }
}
