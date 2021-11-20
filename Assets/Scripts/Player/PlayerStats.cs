using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    [SerializeField] int maxMana=30;
    [SerializeField] int currnetMana;

    [SerializeField] int defence;
    [SerializeField] int dexterity;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
