using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{

    public static ShopManager instance;

    [SerializeField] GameObject shopMenu, buyPanel, sellPanel;
    [SerializeField] TextMeshProUGUI currentCoinsText;

    [SerializeField] List<ItemsManager> itemsForSale;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotBuyContainerParent;
    [SerializeField] Transform itemSlotSellContainerParent;

    [SerializeField] ItemsManager selectedItem;
    [SerializeField] TextMeshProUGUI buyItemName, buyItemDescription, buyItemValue;
    [SerializeField] TextMeshProUGUI sellItemName, sellItemDescription, sellItemValue;
    private float sellProfit = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShopMenu()
    {
        shopMenu.SetActive(true);
        GameManager.instance.shopOpened = true;

        currentCoinsText.text = "Coins: " + GameManager.instance.GetCurrentCoins();
        buyPanel.SetActive(true);
        UpdateItemsInShop(itemSlotBuyContainerParent, itemsForSale);
    }

    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopOpened = false;
    }

    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);

        UpdateItemsInShop(itemSlotBuyContainerParent,itemsForSale);
    }

    public void OpenSellPanel()
    {
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);

        UpdateItemsInShop(itemSlotSellContainerParent, Inventory.instance.GetItemsList());
    }

    //Every time this method called - Update the Shop Items
    private void UpdateItemsInShop(Transform itemSlotContainerParent, List<ItemsManager> itemsToLookThrough)
    {
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }
        foreach (ItemsManager item in itemsToLookThrough)
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();
            Image itemImage = itemSlot.Find("Items Image").GetComponent<Image>();
            itemImage.sprite = item.itemImage;
            TextMeshProUGUI itemsAmountText = itemSlot.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                itemsAmountText.text = "";
            }
            else
            {
                itemsAmountText.text = "";
            }
            itemSlot.GetComponent<ItemButton>().SetItemOnButton(item);
        }
    }





    public void SetItemsForSale(List<ItemsManager> newItemsForSale)
    {
        itemsForSale = newItemsForSale;
    }


    public void SelectedBuyItem(ItemsManager itemToBuy)
    {
        selectedItem = itemToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value: " + selectedItem.valueInCoins;
    }

    public void SelectedSellItem(ItemsManager itemToSell)
    {
        selectedItem = itemToSell;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = "Value: " + (int)(selectedItem.valueInCoins*sellProfit);
    }

    public void BuyItem()
    {
        if (GameManager.instance.GetCurrentCoins() >= selectedItem.valueInCoins)
        {
            GameManager.instance.SetCurrentCoins(GameManager.instance.GetCurrentCoins() - selectedItem.valueInCoins);
            Inventory.instance.AddItems(selectedItem);

            currentCoinsText.text = "Coins: " + GameManager.instance.GetCurrentCoins();

            AudioManager.instance.PlaySFX(9);
        }
    }

    public void SellItem()
    {
        if (selectedItem)
        {
            GameManager.instance.SetCurrentCoins(GameManager.instance.GetCurrentCoins() + (int)(selectedItem.valueInCoins * sellProfit));
            Inventory.instance.RemoveItem(selectedItem);
            currentCoinsText.text = "Coins: " + GameManager.instance.GetCurrentCoins();
            selectedItem = null;
            OpenSellPanel();

            AudioManager.instance.PlaySFX(9);
        }
    }

    //Getters and Setters
    public GameObject GetBuyPanel()
    {
        return buyPanel;
    }
    public GameObject GetSellPanel()
    {
        return sellPanel;
    }
    public GameObject GetShopMenu()
    {
        return shopMenu;
    }


}
