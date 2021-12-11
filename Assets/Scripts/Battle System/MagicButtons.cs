using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicButtons : MonoBehaviour
{
    [SerializeField] string spellName;
    [SerializeField] int spellCost;
    [SerializeField] TextMeshProUGUI spellNameText, spellCostText;
    // Start is called before the first frame update

    public void Press()
    {
        if (BattleManager.instance.GetCurrentActiveCharacter().GetCurrentMana() >= spellCost)
        {
            BattleManager.instance.GetSpellPanel().SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.GetCurrentActiveCharacter().SetCurrnetMana(BattleManager.instance.GetCurrentActiveCharacter().GetCurrentMana() - spellCost);
        }
        else
        {
            BattleManager.instance.GetBattleNotice().SetText("We don't have enough mane!");
            BattleManager.instance.GetBattleNotice().ActivateBattleNotification();
            BattleManager.instance.GetSpellPanel().SetActive(false);
        }

       
    }
    //Getters and Setters
    public string GetSpellName()
    {
        return spellName;
    }
    public int GetSpellCost()
    {
        return spellCost;
    }
    public void SetSpellCost(int cost)
    {
        spellCost = cost;
    }
    public void SetSpellName(string name)
    {
        spellName = name;
    }
    public void SetSpellNameText(string name)
    {
        spellNameText.text = name;
    }
    public void SetSpellCostText(string name)
    {
        spellCostText.text = name;
    }


}
