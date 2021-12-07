using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    public string[] sentences;
    private bool canActiveBox;

    [SerializeField] bool shouldActivateQuest;
    [SerializeField] bool markAsComplete;
    [SerializeField] string questToMark;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canActiveBox && Input.GetButtonDown("Fire1") && !DialogController.instacne.IsDialogBoxActive())
        {
            DialogController.instacne.ActivateDialog(sentences);

            if (shouldActivateQuest)
            {
                DialogController.instacne.ActivateQuestAtEnd(questToMark, markAsComplete);
            }
        }
    }

    //Check if player tag is around the npc
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActiveBox = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActiveBox = false;
        }
    }
}
