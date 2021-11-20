using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string playerName;

    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;
    [SerializeField] int maxLevel = 50;
    [SerializeField] int[] xpForEachLevel;
    [SerializeField] int baseLevelXP = 100;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    [SerializeField] int maxMana=30;
    [SerializeField] int currnetMana;

    [SerializeField] int defence;
    [SerializeField] int dexterity;



    // Start is called before the first frame update
    void Start()
    {
        xpForEachLevel = new int[maxLevel];
        xpForEachLevel[1] = baseLevelXP;
        for(int i = 2; i < xpForEachLevel.Length; i++)
        {
            xpForEachLevel[i] = i * baseLevelXP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
