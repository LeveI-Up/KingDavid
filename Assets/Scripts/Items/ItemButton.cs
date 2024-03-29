﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField] ItemsManager itemOnButton;
    public void Press()
    {
        if (MenuManager.instance.GetMenu().activeInHierarchy)
        {
            MenuManager.instance.GetItemName().text = itemOnButton.itemName;
            MenuManager.instance.GetItemDescription().text = itemOnButton.itemDescription;

            MenuManager.instance.SetActiveItem(itemOnButton);
        }
        if (ShopManager.instance.GetShopMenu().activeInHierarchy)
        {
            if (ShopManager.instance.GetBuyPanel().activeInHierarchy)
            {
                ShopManager.instance.SelectedBuyItem(itemOnButton);
            }
            else if (ShopManager.instance.GetSellPanel().activeInHierarchy)
            {
                ShopManager.instance.SelectedSellItem(itemOnButton);
            }
        }
        if (BattleManager.instance.GetItemToUseMenu().activeInHierarchy)
        {
            MenuManager.instance.SetActiveItem(itemOnButton);
            BattleManager.instance.SelectedItemToUse(itemOnButton);
        }
    }
    public void SetItemOnButton(ItemsManager newItemOnButton)
    {
        
        itemOnButton = newItemOnButton;
        
    }
}
