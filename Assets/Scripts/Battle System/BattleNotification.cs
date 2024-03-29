﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleNotification : MonoBehaviour
{

    [SerializeField] float timeAlive;
    [SerializeField] TextMeshProUGUI textNotice;

    public void SetText(string text)
    {
        textNotice.text = text;
    }

    public void ActivateBattleNotification()
    {
        gameObject.SetActive(true);
        StartCoroutine(MakeNoticeDissapear());
    }

    IEnumerator MakeNoticeDissapear()
    {
        yield return new WaitForSeconds(timeAlive);
        gameObject.SetActive(false);
    }

}
