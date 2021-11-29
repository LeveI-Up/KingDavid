using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerStats[] playerStats;

    public bool gameMenuOpened, dialogBoxOpned, shopOpened;

    [SerializeField] int currentCoines;
    // Start is called before the first frame update
    void Start()
    {
        //singelton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        playerStats = FindObjectsOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Saved");
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Loaded");
            LoadData();
        }

        if (gameMenuOpened || dialogBoxOpned || shopOpened)
        {
            Player.instance.DeactiveMovement(true);
        }
        else
        {
            Player.instance.DeactiveMovement(false);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.instance.transform.position.z);
    }

    public void LoadData()
    {
        float x = PlayerPrefs.GetFloat("Player_Pos_X");
        float y = PlayerPrefs.GetFloat("Player_Pos_Y");
        float z = PlayerPrefs.GetFloat("Player_Pos_Z");
        Player.instance.transform.position = new Vector3(x,y,z);

    }

    //Getters and Setters
    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
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
