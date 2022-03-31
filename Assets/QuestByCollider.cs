using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestByCollider : MonoBehaviour
{
    [SerializeField]
    private string questToComplete;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            QuestManager.instance.MarkQuestComplete(questToComplete);
        }
    }
}
