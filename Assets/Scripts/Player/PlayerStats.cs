using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string playerName;

    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;
    [SerializeField] int maxLevel = 50;
    [SerializeField] int[] xpForNextLevel;
    [SerializeField] int baseLevelXP = 100;
    [SerializeField] float hpManaGrow = 1.06f;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    [SerializeField] int maxMana=30;
    [SerializeField] int currnetMana;

    [SerializeField] int defence;
    [SerializeField] int dexterity;

    private float[] expCalc = { 0.02f, 3.06f, 105.6f };



    // Start is called before the first frame update
    void Start()
    {
        xpForNextLevel = new int[maxLevel];
        xpForNextLevel[1] = baseLevelXP;
        for(int i = 2; i < xpForNextLevel.Length; i++)
        {
            xpForNextLevel[i] = (int)(expCalc[0]*i*i*i+expCalc[1]*i*i+expCalc[2]*i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddXP(baseLevelXP);
        }
    }

    public void AddXP(int amountOfXp)
    {
        currentXP += amountOfXp;
        if (currentXP > xpForNextLevel[playerLevel])
        {
            currentXP -= xpForNextLevel[playerLevel];
            playerLevel++;
            maxHP = (int)(maxHP * 1.18f);
            currentHP = maxHP;
            maxMana = (int)(maxMana * hpManaGrow);
            currnetMana = maxMana;
            if (playerLevel % 2 == 0)
            {
                dexterity++;
            }
            else
            {
                defence++;
            }
        }
    }
}
