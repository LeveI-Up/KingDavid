using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{

    [SerializeField] ItemsManager itemOnButton;

    public void Press()
    {
        MenuManager.instance.GetItemName().text = itemOnButton.itemName;
        MenuManager.instance.GetItemDescription().text = itemOnButton.itemDescription;

        MenuManager.instance.SetActiveItem(itemOnButton);

    }
    public void SetItemOnButton(ItemsManager newItemOnButton)
    {
        itemOnButton = newItemOnButton;
    }

}
