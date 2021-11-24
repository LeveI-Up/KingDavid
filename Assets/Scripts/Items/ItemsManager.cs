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

    public void UseItem(int charcterToUseOn)
    {
        //add stackable items
        PlayerStats selectedCharcter = GameManager.instance.GetPlayerStats()[charcterToUseOn];
        if(itemType == ItemType.Item)
        {
            if(effectType == effecType.HP)
            {
                selectedCharcter.AddHP(amountOfEffect);
            }
            else if (effectType == effecType.Mana)
            {
                selectedCharcter.AddMana(amountOfEffect);
            }
        }
        //try to add non stackable item
        else if(itemType == ItemType.Weapon)
        {
            //check if the player already got weapon
            if (selectedCharcter.GetEquipedWeaponName() != "")
            {
                Inventory.instance.AddItems(selectedCharcter.GetEquipedWeapon());
            }
            selectedCharcter.EquipWeapon(this);
        }
        else if (itemType == ItemType.Armor)
        {
            if (selectedCharcter.GetEquipedArmorName() != "")
            {
                Inventory.instance.AddItems(selectedCharcter.GetEquipedArmor());
            }
            selectedCharcter.EquipArmor(this);
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
