using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerStats[] playerStats;
    public GameObject[] sceneObjects;
    [SerializeField] int quest;

    private bool loaded, unloaded;
    public bool gameMenuOpened, dialogBoxOpned, shopOpened, battleIsActive,questPanelOpened;

    [SerializeField] int currentCoines;
    // Start is called before the first frame update
    private void OnEnable()
    {
        instance = this;
    }
   
    void Start()
    {
        
        playerStats = FindObjectsOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            for(int i =0; i <= quest; i++)
            {
                QuestManager.instance.MarkQuestComplete(QuestManager.instance.GetQuestNames()[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            
            LoadData();
        }

        if (gameMenuOpened || dialogBoxOpned || shopOpened || battleIsActive || questPanelOpened)
        {
            Player.instance.DeactiveMovement(true);
        }
        else
        {
            Player.instance.DeactiveMovement(false);
        }
    }
    //Save all player data
    public void SaveData()
    {
        SavingPlayerPosition();
        //SavingPlayerStats();
        //SavingPlayerItems();
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetSceneAt(1).name);
        Debug.Log("Saved");

    }
    //loop through all the items in the player inventory saving his position and name, if the item is stackable saving his amount.

    /*private static void SavingPlayerItems()
    {
        PlayerPrefs.SetInt("Number_Of_Items", Inventory.instance.GetItemsList().Count);
        for (int i = 0; i < Inventory.instance.GetItemsList().Count; i++)
        {
            ItemsManager itemInInventory = Inventory.instance.GetItemsList()[i];
            PlayerPrefs.SetString("Item_" + i + "_Name", itemInInventory.itemName);

            if (itemInInventory.GetIsStackable())
            {
                PlayerPrefs.SetInt("Items_" + i + "_Amount", itemInInventory.amount);
            }
        }
    }*/

    //Save Player Stats
    private void SavingPlayerStats()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_Active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_Active", 0);
            }
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_Level", playerStats[i].GetPlayerLevel());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_CurrentXP", playerStats[i].GetCurrentXP());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_MaxHP", playerStats[i].GetMaxHP());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_CurrentHP", playerStats[i].GetCurrentHP());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_MaxMana", playerStats[i].GetMaxMana());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_CurrentMana", playerStats[i].GetCurrnetMana());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_Dexterity", playerStats[i].GetDexterity());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_Defence", playerStats[i].GetDefence());
            PlayerPrefs.SetString("Player_" + playerStats[i].GetPlayerName() + "_EquipedWeapon", playerStats[i].GetEquipedWeaponName());
            PlayerPrefs.SetString("Player_" + playerStats[i].GetPlayerName() + "_EquipedArmor", playerStats[i].GetEquipedArmorName());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_WeaponPower", playerStats[i].GetWeaponPower());
            PlayerPrefs.SetInt("Player_" + playerStats[i].GetPlayerName() + "_ArmorDefence", playerStats[i].GetArmorDefence());
            //Save Items

        }
    }

    //Save Player Postion
    private static void SavingPlayerPosition()
    {

        PlayerPrefs.SetFloat("Player_Pos_X", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.instance.transform.position.z);
        Debug.Log("player transform x is: " + Player.instance.transform.position.x + " and the pos saved is: " + PlayerPrefs.GetFloat("Player_Pos_X"));
        Debug.Log("player transform y is: " + Player.instance.transform.position.y + " and the pos saved is: " + PlayerPrefs.GetFloat("Player_Pos_Y"));
        Debug.Log("player transform z is: " + Player.instance.transform.position.z + " and the pos saved is: " + PlayerPrefs.GetFloat("Player_Pos_Z"));

        Debug.Log("pos Saved");

    }

    public void LoadData()
    {
        LoadLastSave();

        LoadingPlayerPosition();
        //LoadingPlayerStats();
        //LoadingPlayerItems();
        //SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        Debug.Log("Loaded");



    }
    //looping throuh saved items count, getting the items name,assets and amount. adding the items to the inventory using the "AddItems" method (including stackable items).

    /*private static void LoadingPlayerItems()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("Number_Of_Items"); i++)
        {
            string itemName = PlayerPrefs.GetString("Item_" + i + "_Name");
            ItemsManager itemToadd = ItemsAssets.instance.GetItemAsset(itemName);
            int itemAmount = 0;

            if (PlayerPrefs.HasKey("Items_" + i + "_Amount"))
            {
                itemAmount = PlayerPrefs.GetInt("Items_" + i + "_Amount");
            }

            //Inventory.instance.AddItems(itemToadd);
            //Inventory.instance.RemoveItem(itemToadd);

            if (itemToadd.GetIsStackable() && itemAmount > 1)
            {
                itemToadd.amount = itemAmount;
            }
        }
        MenuManager.instance.UpdateItemsInventory();

    }
    */

    //Load Player Stats

    private void LoadingPlayerStats()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_Active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }
            playerStats[i].SetPlayerLevel(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_Level"));
            playerStats[i].SetCurrentXP(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_CurrentXP"));
            playerStats[i].SetMaxHP(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_MaxHP"));
            playerStats[i].SetCurrentHP(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_CurrentHP"));
            playerStats[i].SetMaxMana(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_MaxMana"));
            playerStats[i].SetCurrnetMana(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_CurrentMana"));
            playerStats[i].SetDexterity(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_Dexterity"));
            playerStats[i].SetDefence(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_Defence"));
            playerStats[i].SetEquipedWeaponName(PlayerPrefs.GetString("Player_" + playerStats[i].GetPlayerName() + "_EquipedWeapon"));
            playerStats[i].SetEquipedArmorName(PlayerPrefs.GetString("Player_" + playerStats[i].GetPlayerName() + "_EquipedArmor"));
            playerStats[i].SetWeaponPower(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_WeaponPower"));
            playerStats[i].SetArmorDefence(PlayerPrefs.GetInt("Player_" + playerStats[i].GetPlayerName() + "_ArmorDefence"));

        }
    }

    //Load Player Postion

    public static void LoadingPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("Player_Pos_X");
        float y = PlayerPrefs.GetFloat("Player_Pos_Y");
        float z = PlayerPrefs.GetFloat("Player_Pos_Z");
        Player.instance.transform.position = new Vector3(x, y, z);
        Debug.Log("player transform x is: " + Player.instance.transform.position.x + " and the pos saved is: " + PlayerPrefs.GetFloat("Player_Pos_X"));
        Debug.Log("player transform y is: " + Player.instance.transform.position.y + " and the pos saved is: " + PlayerPrefs.GetFloat("Player_Pos_Y"));
        Debug.Log("player transform z is: " + Player.instance.transform.position.z + " and the pos saved is: " + PlayerPrefs.GetFloat("Player_Pos_Z"));
        Debug.Log("pos Loaded");

    }

    public void LoadLastSave()
    {
        if (!loaded)
        {
            Player.instance.transitionName = PlayerPrefs.GetString("Current_Scene");

            MenuManager.instance.FadeImage();

            StartCoroutine(LoadSavedSceneCoroutine());



            loaded = true;
        }
    }

    IEnumerator LoadSavedSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Current_Scene"), LoadSceneMode.Additive);


        GameManager.instance.LoadData();
        MenuManager.instance.FadeOut();


        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene(SceneManager.GetSceneAt(1).name);
            //Debug.Log("Scene unload called: " + arrivingFrom);
        }

    }


    //Getters and Setters
    public PlayerStats[] GetPlayerStats()
    {
        return FindObjectsOfType<PlayerStats>();
    }
    public int GetCurrentCoins()
    {
        return currentCoines;
    }
    public void SetCurrentCoins(int newCurrentCoins)
    {
        currentCoines = newCurrentCoins;
    }
}
