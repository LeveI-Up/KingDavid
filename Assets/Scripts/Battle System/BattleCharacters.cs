using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{

    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;

    [SerializeField] string charcterName;
    [SerializeField] int currentHP, maxHP, currentMana, maxMana, dexterity, defence, weaponPower, armorDefence;
    [SerializeField] bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
