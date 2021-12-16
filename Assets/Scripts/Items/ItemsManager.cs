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
            AudioManager.instance.PlaySFX(1);
            if (effectType == effecType.HP)
            {
                selectedCharcter.AddHP(amountOfEffect);
                BattleCharacters.instance.SetCurrentHP(BattleCharacters.instance.GetCurrentHP() + amountOfEffect);
            }
            else if (effectType == effecType.Mana)
            {
                selectedCharcter.AddMana(amountOfEffect);
            }
        }
        //try to add non stackable item
        else if(itemType == ItemType.Weapon)
        {
            AudioManager.instance.PlaySFX(0);
            //check if the player already got weapon
            if (selectedCharcter.GetEquipedWeaponName() != "")
            {
                Inventory.instance.AddItems(selectedCharcter.GetEquipedWeapon());
            }
            selectedCharcter.EquipWeapon(this);

        }
        else if (itemType == ItemType.Armor)
        {
            AudioManager.instance.PlaySFX(0);
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
            Inventory.instance.AddItems(this);
            AudioManager.instance.PlaySFX(2);
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
