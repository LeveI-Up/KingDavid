using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//source of inf, wont be attach to notihng


[System.Serializable]

public class BattleMoves
{
    [SerializeField] string moveName;
    [SerializeField] int movePower, manaCost;
    [SerializeField] AttackEffect effectToUse;


    //Getters and Setters
    public string GetMoveName()
    {
        return moveName;
    }
    public int GetMovePower()
    {
        return movePower;
    }
    public int GetManaCost()
    {
        return manaCost;
    }
    public AttackEffect GetEffectToUse()
    {
        return effectToUse;
    }

}
