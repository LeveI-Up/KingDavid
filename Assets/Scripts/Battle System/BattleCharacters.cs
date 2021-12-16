using System;
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

    [SerializeField] Sprite deadSprite;
    [SerializeField] ParticleSystem deathEffect;
    public static BattleCharacters instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayer && isDead)
        {
            FadeOutEnemy();
        }
    }

    private void FadeOutEnemy()
    {
        GetComponent<SpriteRenderer>().color = new Color(
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.r, 1f,0.3f*Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.g, 0f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.b, 0f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.a, 0f, 0.3f * Time.deltaTime)
            );
        if (GetComponent<SpriteRenderer>().color.a == 0){
            gameObject.SetActive(false);
        }
    }
    public void KillEnemy()
    {
        isDead = true;
    }

    public void TakeHPDamage(int damageToReceive)
    {
        currentHP -= damageToReceive;
        if (currentHP < 0)
            currentHP = 0;
    }

    public void UseItemInBattle(ItemsManager itemToUse)
    {
        if(itemToUse.itemType == ItemsManager.ItemType.Item)
        {
            if(itemToUse.effectType == ItemsManager.effecType.HP)
            {
                AddHP(itemToUse.amountOfEffect);
            }
            else if (itemToUse.effectType == ItemsManager.effecType.Mana)
            {
                AddMana(itemToUse.amountOfEffect);
            }
            else if (itemToUse.itemType == ItemsManager.ItemType.Weapon)
            {
                print("You can use only potions durning a battle.");
            }
        }
    }

    private void AddMana(int amountOfEffect)
    {
        currentMana += amountOfEffect;
    }

    private void AddHP(int amountOfEffect)
    {
        currentHP += amountOfEffect;
    }

    public void KillPlayer()
    {
        if (deadSprite)
        {
            GetComponent<SpriteRenderer>().sprite = deadSprite;
            Instantiate(deathEffect, transform.position, transform.rotation);
            isDead = true;
        }
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
    public bool GetIsDead()
    {
        return isDead;
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
