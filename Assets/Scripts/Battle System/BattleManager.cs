using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


    [SerializeField] BattleMoves[] battleMovesList;
    [SerializeField] ParticleSystem characterAttackEffect;
    [SerializeField] CharacterDamageGUI damageText;
    [SerializeField] GameObject[] playersBattleStats;
    [SerializeField] TextMeshProUGUI[] playersNameText;
    [SerializeField] Slider[] playerHealthSlider, PlayerManaSlider;
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
        CheckForUIButtons();
    }
    //check if its the player turn - show UI buttons.
    private void CheckForUIButtons()
    {
        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].GetIsPlayer())
                    UIButtonHolder.SetActive(true);
                else
                {
                    UIButtonHolder.SetActive(false);
                    StartCoroutine(EnemyTurnsCoroutine());
                }
            }
        }
    }

    //start battle with the Enemies in the current scene
    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            SettingUpBattle();
            AddingPlayers();
            AddingEnemies(enemiesToSpawn);
            UpdatePlayerStats();
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
    //Increase the turn after the Player/Enemies choose action.
    private void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeCharacters.Count)
        {
            currentTurn = 0;
        }
        waitingForTurn = true;
        UpdateBattle();
        UpdatePlayerStats();
    }
    //Check the state of the players and ememies (dead or alive)
    private void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayersAreDead = true;

        for(int i=0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].GetCurrentHP() <= 0)
            {
                activeCharacters[i].SetCurrentHP(0);
            }
            if (activeCharacters[i].GetCurrentHP() == 0)
            {
                activeCharacters[i].gameObject.SetActive(false);
            }
            else
            {
                if (activeCharacters[i].GetIsPlayer())
                {
                    allPlayersAreDead = false;
                }
                else
                {
                    allEnemiesAreDead = false;
                }
            }
        }
        if(allPlayersAreDead || allEnemiesAreDead)
        {
            if (allPlayersAreDead)
                print("We Lost");
            else if(allEnemiesAreDead)
                print("We Won");

            battleScene.SetActive(false);
            GameManager.instance.battleIsActive = false;
            isBattleActive = false;
        }
        else
        {
            while (activeCharacters[currentTurn].GetCurrentHP() == 0) // if player died - skip his turn.
            {
                currentTurn++;
                if (currentTurn >= activeCharacters.Count)
                    currentTurn = 0;
            }
        }

    }
    //Wait time for the enemies to attack
    public IEnumerator EnemyTurnsCoroutine()
    {
        waitingForTurn = false;

        yield return new WaitForSeconds(2f);
        EnemyAttack();
        yield return new WaitForSeconds(2f);
        NextTurn();
    }
    //Choose the target (player) that the enemy attack from all the players at the battle and attack the target
    private void EnemyAttack()
    {
        List<int> players = new List<int>();
        

        for(int i=0; i < activeCharacters.Count; i++)
        {
            if(activeCharacters[i].GetIsPlayer() && activeCharacters[i].GetCurrentHP() > 0)
            {
                players.Add(i);
            }
        }
        
        int selectedPlayerToAttack = players[UnityEngine.Random.Range(0, players.Count)]; //random select player
        int selectedAttack = UnityEngine.Random.Range(0, activeCharacters[currentTurn].GetAttacksAvaliable().Length); //random select attack
        int movePower = 0;
        for(int i=0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].GetMoveName() == activeCharacters[currentTurn].GetAttacksAvaliable()[selectedAttack]) //check if the enemy got the attack
            {
                Instantiate(
                    battleMovesList[i].GetEffectToUse(),
                    activeCharacters[selectedPlayerToAttack].transform.position,
                    activeCharacters[selectedPlayerToAttack].transform.rotation
                );

                movePower = battleMovesList[i].GetMovePower();
            }
        }

        //Instantiate the effect on the attack character.
        Instantiate(
            characterAttackEffect,
            activeCharacters[currentTurn].transform.position,
            activeCharacters[currentTurn].transform.rotation
            );
        DealDamageToCharacters(selectedPlayerToAttack, movePower);

        UpdatePlayerStats();
        
    }
    //Calculates the amount of the attack damage, the amount of the hit damage and Instantiate the damage text on the target.
    private void DealDamageToCharacters(int selectedCharacterToAttack,int movePower)
    {
        float attackPower = activeCharacters[currentTurn].GetDexterity() + activeCharacters[currentTurn].GetWeaponPower();//the attacker
        float defencePower = activeCharacters[selectedCharacterToAttack].GetDefence() + activeCharacters[selectedCharacterToAttack].GetArmorDefence();//the target

        float damageAmount = (attackPower / defencePower) * movePower * UnityEngine.Random.Range(0.9f,1.1f); //random between 90% hit to 110% hit
        int damageToGive = (int)damageAmount;

        damageToGive = CalculateCritical(damageToGive); // chance to have critical hit

        Debug.Log(activeCharacters[currentTurn].GetCharcterName() + " just dealt " + damageAmount + "(" + damageToGive + ") to "
            + activeCharacters[selectedCharacterToAttack]);

        activeCharacters[selectedCharacterToAttack].TakeHPDamage(damageToGive);

        CharacterDamageGUI CharacterDamageText = Instantiate(
            damageText,
            activeCharacters[selectedCharacterToAttack].transform.position,
            activeCharacters[selectedCharacterToAttack].transform.rotation
        );
        CharacterDamageText.SetDamage(damageToGive);

    }
    //Calculate critical damage (20% to hit)
    private int CalculateCritical(int damageToGive)
    {
        if(UnityEngine.Random.value<= 0.2f)
        {
            Debug.Log("CRITICAL HIT instaed of " + damageToGive + " points. " + (damageToGive * 2) + " was dealt");
            return (damageToGive * 2);
        }
        return damageToGive;
    }
    //Update players battle stats (name,mana slider, hp slider).
    public void UpdatePlayerStats()
    {
        for(int i = 0; i < playersNameText.Length; i++)
        {
            if (activeCharacters.Count > i)
            {
                if (activeCharacters[i].GetIsPlayer())
                {
                    BattleCharacters playerData = activeCharacters[i];
                    playersNameText[i].text = playerData.GetCharcterName();
                    playerHealthSlider[i].maxValue = playerData.GetMaxHP();
                    playerHealthSlider[i].value = playerData.GetCurrentHP();
                    PlayerManaSlider[i].maxValue = playerData.GetMaxMana();
                    PlayerManaSlider[i].value = playerData.GetCurrentMana();
                }
                else
                {
                    playersBattleStats[i].SetActive(false);
                }
            }
            else
            {
                playersBattleStats[i].SetActive(false);
            }
        }
    }
}
