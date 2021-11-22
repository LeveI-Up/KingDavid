using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;

    public static MenuManager instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, xpText, levelText,charLevelText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] charcterImage;
    [SerializeField] GameObject[] charcterPanel;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (menu.activeInHierarchy)
            {
                
                menu.SetActive(false);
                GameManager.instance.gameMenuOpened = false;
            }
            else
            {
                UpdateStats();
                menu.SetActive(true);
                GameManager.instance.gameMenuOpened = true;
            }
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
    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fading");

    }


}
