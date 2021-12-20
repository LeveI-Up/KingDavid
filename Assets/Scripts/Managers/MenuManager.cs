using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;




public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu,itemsPanel,statsPanel;
    

    public static MenuManager instance;
    //player stats at the menu screen
    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, xpText, levelText, charLevelText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] charcterImage;
    [SerializeField] GameObject[] charcterPanel;
    //player stats at the stats button
    [SerializeField] GameObject[] statsButtons;
    [SerializeField] TextMeshProUGUI statName, statHp, statMana, statDex, statDef, statEquipedWeapon, statEquipedArmor, statWeaponPower, statArmorDefence;
    [SerializeField] Image charcterStatImage;
    //Inventory & items
    [SerializeField] GameObject itemSlotContainer;
    //[SerializeField] Transform itemSlotContainerParent;
    [SerializeField] Transform itemSlotTest;
    [SerializeField] ItemsManager activeItem;
    [SerializeField] TextMeshProUGUI itemName, itemDescription;
    [SerializeField] GameObject charcterChoicePanel;
    [SerializeField] TextMeshProUGUI[] itemsCharcterChoiceNames;
    

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpdateItemsInventory();
            if (menu.activeInHierarchy)
            {
                
                menu.SetActive(false);
                itemsPanel.SetActive(false);
                statsPanel.SetActive(false);
                GameManager.instance.gameMenuOpened = false;
                AudioManager.instance.PlaySFX(11);
            }
            else
            {
                UpdateStats();
                menu.SetActive(true);
                GameManager.instance.gameMenuOpened = true;
                AudioManager.instance.PlaySFX(10);
            }
            
        }
        if (!menu.activeInHierarchy)
        {
            GameManager.instance.gameMenuOpened = false;
        }
     
    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();

        for(int i = 0; i < playerStats.Length; i++)
        {
            charcterPanel[i].SetActive(true);

            nameText[i].text = playerStats[i].GetPlayerName();
            hpText[i].text ="HP: "+  playerStats[i].GetCurrentHP()+"/"+ playerStats[i].GetMaxHP();
            manaText[i].text = "Mana: " + playerStats[i].GetCurrnetMana() + "/" + playerStats[i].GetMaxMana();
            levelText[i].text = "Current XP: " + playerStats[i].GetCurrentXP();
            charcterImage[i].sprite = playerStats[i].GetCharcterImage();
            charLevelText[i].text ="Level: " + playerStats[i].GetPlayerLevel().ToString();
            xpText[i].text = playerStats[i].GetCurrentXP().ToString() + "/" + playerStats[i].GetXpForNextLevel()[playerStats[i].GetPlayerLevel()];

            xpSlider[i].maxValue = playerStats[i].GetXpForNextLevel()[playerStats[i].GetPlayerLevel()];
            xpSlider[i].value = playerStats[i].GetCurrentXP();

        }
    }
    //update the details on stats button
    public void StatsMenu()
    {
        for(int i=0; i < playerStats.Length; i++)
        {
            statsButtons[i].SetActive(true);
            statsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].GetPlayerName();
        }
        StatsMenuUpdate(0);
    }

    //update stats on the menu screen
    public void StatsMenuUpdate(int playerSelectedNumber)
    {
        PlayerStats playerSelected = playerStats[playerSelectedNumber];
        statName.text = playerSelected.GetPlayerName();
        statHp.text = playerSelected.GetCurrentHP().ToString() + "/"+ playerSelected.GetMaxHP();
        statMana.text = playerSelected.GetCurrnetMana().ToString() + "/" + playerSelected.GetMaxMana();
        statDex.text = playerSelected.GetDexterity().ToString();
        statDef.text = playerSelected.GetDefence().ToString();
        charcterStatImage.sprite = playerSelected.GetCharcterImage();
        statEquipedWeapon.text = playerSelected.GetEquipedWeaponName();
        statEquipedArmor.text = playerSelected.GetEquipedArmorName();
        statWeaponPower.text = playerSelected.GetWeaponPower().ToString();
        statArmorDefence.text = playerSelected.GetArmorDefence().ToString();


    }
    //once this method called - update all the items in the inventory.
    public void UpdateItemsInventory()
    {
       
        
        //Debug.Log(itemSlotContainerParent.childCount);
        
        foreach (Transform itemSlot in itemSlotTest)
        {
          
            Destroy(itemSlot.gameObject);

        }
        
            foreach (ItemsManager item in Inventory.instance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotTest).GetComponent<RectTransform>();
            Image itemImage = itemSlot.Find("Items Image").GetComponent<Image>();
            itemImage.sprite = item.itemImage;
            TextMeshProUGUI itemsAmountText = itemSlot.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                itemsAmountText.text = item.amount.ToString();
            }
            else
            {
                itemsAmountText.text = "";
            }
            

            itemSlot.GetComponent<ItemButton>().SetItemOnButton(item);
            
        }
        
    }

    //Quit the game(not working on unity)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }
    public void DiscardItem()
    {
        Inventory.instance.RemoveItem(activeItem);
        UpdateItemsInventory();
        AudioManager.instance.PlaySFX(3);
    }

    public void UseItem(int selectedCharcter)
    {
        if (GameManager.instance.battleIsActive)
        {
            
                activeItem.UseItem(selectedCharcter);
                Inventory.instance.RemoveItem(activeItem);
                UpdateItemsInventory();
                BattleManager.instance.UpdatePlayerStats();
                activeItem = null;  
        }
        else
        {
            activeItem.UseItem(selectedCharcter);
            OpenCharcterChoicePanel();
            //discard the item
            Inventory.instance.RemoveItem(activeItem);
            UpdateItemsInventory();


            activeItem = null;
        }
        

    }

    //open charcter panel when the player try to use item
    public void OpenCharcterChoicePanel()
    {
        if (activeItem)
        {
            charcterChoicePanel.SetActive(true);
            for (int i = 0; i < playerStats.Length; i++)
            {
                PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];
                itemsCharcterChoiceNames[i].text = activePlayer.GetPlayerName();
                bool activePlayerAvailbale = activePlayer.gameObject.activeInHierarchy;
                itemsCharcterChoiceNames[i].transform.parent.gameObject.SetActive(activePlayerAvailbale);

            }
        }
    }

   /* public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
   */
    public void CloseCharcterChoicePanel()
    {
        charcterChoicePanel.SetActive(false);
    }

    public TextMeshProUGUI GetItemName()
    {
        return itemName;
    }
    public TextMeshProUGUI GetItemDescription( )
    {
        return itemDescription;
    }
    public void SetActiveItem( ItemsManager newActiveItem)
    {
        activeItem = newActiveItem;
    }
    public GameObject GetMenu()
    {
        return menu;
    }
    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fading");

    }
    public void FadeOut()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("End Fading");

    }



}
