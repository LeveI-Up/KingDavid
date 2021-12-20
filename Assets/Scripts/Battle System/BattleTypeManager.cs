using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleTypeManager 
{
    [SerializeField] string[] namesOfEnemies;
    [SerializeField] int rewardXP;
    [SerializeField] ItemsManager[] rewardItems;


    public string[] GetNamesOfEnemies()
    {
        return namesOfEnemies;
    }
    public int GetRewardXP()
    {
        return rewardXP;
    }
    public ItemsManager[] GetRewardItems()
    {
        return rewardItems;
    }
}
