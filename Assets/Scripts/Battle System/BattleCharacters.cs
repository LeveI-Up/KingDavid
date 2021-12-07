using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{

    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;

    [SerializeField] string charcterName;
    [SerializeField] int currentHP, maxHP, currentMana, maxMana, dexterity, defence, weaponPower, armorDefence;
    [SerializeField] bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHPDamage(int damageToReceive)
    {
        currentHP -= damageToReceive;
        if (currentHP < 0)
            currentHP = 0;
    }

    //Getters and Setters
    public bool GetIsPlayer()
    {
        return isPlayer;
    }
    public string GetCharcterName()
    {
        return charcterName;
    }
    
    public int GetMaxHP()
    {
        return maxHP;
    }
    public int GetCurrentHP()
    {
        return currentHP;
    }
    public int GetMaxMana()
    {
        return maxMana;
    }
    public int GetCurrentMana()
    {
        return currentMana;
    }
    public int GetDefence()
    {
        return defence;
    }
    public int GetDexterity()
    {
        return dexterity;
    }
    public string[] GetAttacksAvaliable()
    {
        return attacksAvailable;
    }


    public int GetWeaponPower()
    {
        return weaponPower;
    }
    public int GetArmorDefence()
    {
        return armorDefence;
    }

    public void SetMaxHP(int stats)
    {
        maxHP = stats;
    }
    public void SetCurrentHP(int stats)
    {
        currentHP = stats;
    }
    public void SetMaxMana(int stats)
    {
        maxMana = stats;
    }
    public void SetCurrnetMana(int stats)
    {
        currentMana = stats;
    }
    public void SetDefence(int stats)
    {
        defence = stats;
    }
    public void SetDexterity(int stats)
    {
        dexterity = stats;
    }
    public void SetWeaponPower(int stats)
    {
        weaponPower = stats;
    }
    public void SetArmorDefence(int stats)
    {
        armorDefence = stats;
    }

}
