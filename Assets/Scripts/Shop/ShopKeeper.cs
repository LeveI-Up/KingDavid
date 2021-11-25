using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpenShop;
    [SerializeField] List<ItemsManager> itemsForSale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpenShop && Input.GetButtonDown("Fire1") && !(Player.instance.GetDeactiveMovement()) && !ShopManager.instance.GetShopMenu().activeInHierarchy)
        {
            ShopManager.instance.SetItemsForSale(itemsForSale);
            ShopManager.instance.OpenShopMenu();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpenShop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpenShop = false;
        }
    }
}
