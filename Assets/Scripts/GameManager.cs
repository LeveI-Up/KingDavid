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
        if(gameMenuOpened || dialogBoxOpned || shopOpened)
        {
            Player.instance.DeactiveMovement(true);
        }
        else
        {
            Player.instance.DeactiveMovement(false);
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }
    public int GetCurrentCoins()
    {
        return currentCoines;
    }
}
