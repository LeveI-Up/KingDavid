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
       
    
        
    }
}
