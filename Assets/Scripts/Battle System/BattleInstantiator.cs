using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInstantiator : MonoBehaviour
{
    public static BattleInstantiator instance;
    [SerializeField] BattleTypeManager[] availableBattles;
    [SerializeField] bool activateOnEnter;
    private bool inArea;

    [SerializeField] float timeBetweenBattles;
    private float battleCounter;
    [SerializeField] bool deactivateBattleAfterStart;
    [SerializeField] int musicToPlay;

    [SerializeField] bool canRunAway;

    [SerializeField] bool shouldCompleteQuest;
    [SerializeField] string questToCompleteName;

    
    

    private void Start()
    {
        instance = this;
        battleCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
    }

    private void Update()
    {
        if(inArea && !Player.instance.GetDeactiveMovement())
        {
            if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") != 0)
            {
                battleCounter -= Time.deltaTime;
            }
        }
        if (battleCounter <= 0)
        {
            battleCounter = battleCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
            StartCoroutine(StartBattleCoroutine());
        }
    }

    private IEnumerator StartBattleCoroutine()
    {
        MenuManager.instance.FadeImage();
        AudioManager.instance.PlayBackgroundMusic(musicToPlay);
        GameManager.instance.battleIsActive = true;
        int selectedBattle = Random.Range(0, availableBattles.Length);
        BattleManager.instance.SetItemsReward(availableBattles[selectedBattle].GetRewardItems());
        BattleManager.instance.SetXpRewardAmount(availableBattles[selectedBattle].GetRewardXP());

        

        yield return new WaitForSeconds(2f);
        MenuManager.instance.FadeOut();
        BattleManager.instance.StartBattle(availableBattles[selectedBattle].GetNamesOfEnemies(),canRunAway);

        if (deactivateBattleAfterStart)
            Destroy(gameObject);
        
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activateOnEnter)
            {
                StartCoroutine(StartBattleCoroutine());
            }
            else
            {
                inArea = true;
            }
        }
    }
    public string GetQuest()
    {
        return questToCompleteName;

    }
    public bool GetQuestBool()
    {
        return shouldCompleteQuest;
    }
}
