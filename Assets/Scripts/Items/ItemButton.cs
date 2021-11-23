using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{

    private ItemsManager itemOnButton;

    public void Press()
    {
        MenuManager.instance.itemName.text = itemOnButton.itemName;
        MenuManager.instance.itemDescription.text = itemOnButton.itemDescription;

    }
    public void SetItemOnButton(ItemsManager newItemOnButton)
    {
        itemOnButton = newItemOnButton;
    }

}
