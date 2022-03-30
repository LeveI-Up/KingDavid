﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<ItemsManager> itemsList;
    // Start is called before the first frame update

    void Start()
    {
      
        itemsList = new List<ItemsManager>();
        

        //Debug.Log("new Inventory created");
    }

    // Update is called once per frame
    void Update()
    {
        instance = this;
    }
    public void AddItems(ItemsManager item)
    {
        if (item.GetIsStackable())
        {
            Debug.Log("Im stackable");
            bool itemAlreadyInInventory = false;
            foreach(ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount++;
                    Debug.Log("Im here");
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemsList.Add(item);
                Debug.Log("wasnt in inventory");
            }
        }
        else
        {
            itemsList.Add(item);
        }
        MenuManager.instance.UpdateItemsInventory();

    }
    
    public void RemoveItem(ItemsManager item)
    {
        if (item.GetIsStackable())
        {
            ItemsManager inventoryItem = null;
            foreach(ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }
            if(inventoryItem != null && inventoryItem.amount <= 0)
            {
                itemsList.Remove(inventoryItem);
            }
        }
        else
        {
            itemsList.Remove(item);
        }
    }

    public List<ItemsManager> GetItemsList()
    {
        return itemsList;
    }
}
