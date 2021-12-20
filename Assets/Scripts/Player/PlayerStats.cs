using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    [SerializeField] string playerName;

    [SerializeField] Sprite charcterImage;

    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;
    [SerializeField] int maxLevel = 50;
    [SerializeField] int[] xpForNextLevel;
    [SerializeField] int baseLevelXP = 100;
    private float hpGrow = 1.18f;
    private float manaGrow = 1.06f;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    [SerializeField] int maxMana=30;
    [SerializeField] int currnetMana;

    [SerializeField] int defence;
    [SerializeField] int dexterity;
    [SerializeField] string equipedWeaponName;
    [SerializeField] string equipedArmorName;
    [SerializeField] int weaponPower;
    [SerializeField] int armorDefence;
    [SerializeField] ItemsManager equipedWeapon, equipedArmor;


    //Unique Formula to calc the exp
    private float[] expCalc = { 0.02f, 3.06f, 105.6f };



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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

    }


    public void AddXP(int amountOfXp)
    {
        currentXP += amountOfXp;
        if (currentXP > xpForNextLevel[playerLevel])
        {
            currentXP -= xpForNextLevel[playerLevel];
            playerLevel++;
            maxHP = (int)(maxHP * hpGrow);
            currentHP = maxHP;
            maxMana = (int)(maxMana * manaGrow);
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

    public void AddHP(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
    public void AddMana(int amount)
    {
        currnetMana += amount;
        if (currnetMana > maxMana)
        {
            currnetMana = maxMana;
        }
    }

    public void EquipWeapon(ItemsManager weaponToEquip)
    {
        equipedWeapon = weaponToEquip;
        equipedWeaponName = equipedWeapon.itemName;
        weaponPower = equipedWeapon.weaponAttackPower;
    }

    public void EquipArmor(ItemsManager armorToEquip)
    {
        equipedArmor = armorToEquip;
        equipedArmorName = equipedArmor.itemName;
        armorDefence = equipedArmor.ArmorDef;
    }




    //Getters and Setters
    public string GetPlayerName()
    {
        return playerName;
    }
    public Sprite GetCharcterImage()
    {
        return charcterImage;
    }
    public int GetPlayerLevel()
    {
        return playerLevel;
    }
    public int GetCurrentXP()
    {
        return currentXP;
    }
    public int GetMaxLevel()
    {
        return maxLevel;
    }
    public int[] GetXpForNextLevel()
    {
        return xpForNextLevel;
    }
    public int GetBaseLevelXP()
    {
        return baseLevelXP;
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
    public int GetCurrnetMana()
    {
        return currnetMana;
    }
    public int GetDefence()
    {
        return defence;
    }
    public int GetDexterity()
    {
        return dexterity;
    }
    public string GetEquipedWeaponName()
    {
        return equipedWeaponName;
    }
    public string GetEquipedArmorName()
    {
        return equipedArmorName;
    }
    public int GetWeaponPower()
    {
        return weaponPower;
    }
    public int GetArmorDefence()
    {
        return armorDefence;
    }
    public ItemsManager GetEquipedWeapon()
    {
        return equipedWeapon;
    }
    public ItemsManager GetEquipedArmor()
    {
        return equipedArmor;
    }




    public void SetPlayerLevel(int stats)
    {
        playerLevel = stats;
    }
    public void SetCurrentXP(int stats)
    {
        currentXP = stats;
    }
    public void SetMaxHP(int stats)
    {
         maxHP=stats;
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
         currnetMana = stats;
    }
    public void SetDefence(int stats)
    {
         defence = stats;
    }
    public void SetDexterity(int stats)
    {
         dexterity = stats;
    }
    public void SetEquipedWeaponName(string stats)
    {
         equipedWeaponName = stats;
    }
    public void SetEquipedArmorName(string stats)
    {
         equipedArmorName = stats;
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
