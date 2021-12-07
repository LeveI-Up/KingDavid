using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleTargetButtons : MonoBehaviour
{

    [SerializeField] string moveName;
    [SerializeField] int activeBattleTarget;
    [SerializeField] TextMeshProUGUI targetName;

    // Start is called before the first frame update
    void Start()
    {
        targetName = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void Press()
    {
        BattleManager.instance.PlayerAttack(moveName, activeBattleTarget);
    }

    //Getters and Setters
    public void SetMoveName(string newMoveName)
    {
        moveName = newMoveName;
    }
    public void SetActiveBattleTarget(int newActiveBattleTarget)
    {
        activeBattleTarget = newActiveBattleTarget;
    }
    public void SetTargetName(string newTargetName)
    {
        targetName.text = newTargetName;
    }
}
