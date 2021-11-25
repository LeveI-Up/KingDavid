using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{

    public static ShopManager instance;

    [SerializeField] GameObject shopMenu, buyPanel, sellPanel;
    [SerializeField] TextMeshProUGUI currentCoinsText;

    [SerializeField] List<ItemsManager> itemsForSale;
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
    }

    public void OpenSellPanel()
    {
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);
    }

    public GameObject GetShopMenu()
    {
        return shopMenu;
    }

    public void SetItemsForSale(List<ItemsManager> newItemsForSale)
    {
        itemsForSale = newItemsForSale;
    }
}
