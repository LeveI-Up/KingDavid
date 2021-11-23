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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Destroy(gameObject);
    }
    public bool GetIsStackable()
    {
        return isStackable;
    }

}
