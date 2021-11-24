using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{

    public enum ItemType { Item, Weapon, Armor}
    public ItemType itemType;
    public string itemName, itemDescription;
    public int valueInCoins;
    public Sprite itemImage;
    public int amountOfEffect;
    public enum effecType { HP,Mana};
    public effecType effectType;
    public int weaponAttackPower, ArmorDef;

    [SerializeField] bool isStackable;
    public int amount;

    public void UseItem()
    {
        if(itemType == ItemType.Item)
        {
            if(effectType == effecType.HP)
            {
                PlayerStats.instance.AddHP(amountOfEffect);
            }
            else if (effectType == effecType.Mana)
            {
                PlayerStats.instance.AddMana(amountOfEffect);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //print("This item is " + itemName);
            Inventory.instance.AddItems(this);
            SelfDestroy();
        }
    }
    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
    public bool GetIsStackable()
    {
        return isStackable;
    }

}
