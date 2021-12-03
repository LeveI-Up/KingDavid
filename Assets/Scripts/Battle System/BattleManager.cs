using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;
    private bool isBattleActive;
    [SerializeField] GameObject battleScene;
    [SerializeField] Transform[] playersPosition, enemiesPosition;
    [SerializeField] BattleCharacters[] playersPrefabs, enemiesPrefabs;
    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();
    private float charViewInGameMode = 55f; //sprite has to be in pos.z = 52+ to be shown in the "game" layer

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIButtonHolder;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartBattle(new string[] { "Mage Master", "Blueface", "Mage", "Warlock" });
            
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }
        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].GetIsPlayer())
                {
                    UIButtonHolder.SetActive(true);
                }
                else
                {
                    UIButtonHolder.SetActive(false);
                }
            }
        }
    }

    private void NextTurn()
    {
        currentTurn++;
        if(currentTurn>= activeCharacters.Count)
        {
            currentTurn = 0;
        }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            SettingUpBattle();
            AddingPlayers();
            AddingEnemies(enemiesToSpawn);
            waitingForTurn = true;
            currentTurn = 0;  //UnityEngine.Random.Range(0, activeCharacters.Count);
        }

    }
    //add all the Enemies with PlayersStats script in the current scene to the battle

    private void AddingEnemies(string[] enemiesToSpawn)
    {
        for (int i = 0; i < enemiesPrefabs.Length; i++)
        {
            if (enemiesToSpawn[i] != "")
            {
                for (int j = 0; j < enemiesPrefabs.Length; j++)
                {
                    if (enemiesPrefabs[j].GetCharcterName() == enemiesToSpawn[i])
                    {
                        BattleCharacters newEnemy = Instantiate(
                            enemiesPrefabs[j],
                            enemiesPosition[i].position,
                            enemiesPosition[i].rotation,
                            enemiesPosition[i]
                      );
                        activeCharacters.Add(newEnemy);
                    }
                }
            }
        }
    }

    //add all the players with PlayersStats script in the current scene to the battle
    private void AddingPlayers()
    {
        for (int i = 0; i < GameManager.instance.GetPlayerStats().Length; i++)
        {
            if (GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playersPrefabs.Length; j++)
                {
                    if (playersPrefabs[j].GetCharcterName() == GameManager.instance.GetPlayerStats()[i].GetPlayerName())
                    {
                        BattleCharacters newPlayer = Instantiate(
                            playersPrefabs[j],
                            playersPosition[i].position,
                            playersPosition[i].rotation,
                            playersPosition[i]
                      );

                        activeCharacters.Add(newPlayer);
                        ImportPlayerStats(i);

                    }
                }
            }
        }
    }

    //Update all the stats for all the BattleCharcters in the current scene
    private void ImportPlayerStats(int i)
    {
        PlayerStats player = GameManager.instance.GetPlayerStats()[i];
        activeCharacters[i].SetCurrentHP(player.GetCurrentHP());
        activeCharacters[i].SetMaxHP(player.GetMaxHP());
        activeCharacters[i].SetCurrnetMana(player.GetCurrnetMana());
        activeCharacters[i].SetMaxMana(player.GetMaxMana());
        activeCharacters[i].SetDefence(player.GetDefence());
        activeCharacters[i].SetDexterity(player.GetDexterity());
        activeCharacters[i].SetWeaponPower(player.GetWeaponPower());
        activeCharacters[i].SetArmorDefence(player.GetArmorDefence());
    }
    //setting up the battle enviroment positions
    private void SettingUpBattle()
    {
        isBattleActive = true;
        GameManager.instance.battleIsActive = true;
        transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            charViewInGameMode); //2d game
        battleScene.SetActive(true);
        
    }
}
