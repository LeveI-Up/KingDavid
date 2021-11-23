using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<ItemsManager> itemsList;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        itemsList = new List<ItemsManager>();
        //Debug.Log("new Inventory created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItems(ItemsManager item)
    {
        if (item.GetIsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemsList.Add(item);
            }
        }
        else
        {
            itemsList.Add(item);
        }
        

    }

    public List<ItemsManager> GetItemsList()
    {
        return itemsList;
    }
}
