using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText, nameText;
    [SerializeField] GameObject dialogBox, nameBox;
    [SerializeField] string[] dialogSentences;
    [SerializeField] int currentSentence;
    public static DialogController instacne;
    private bool dialogJustStarted;

    private string questToMark;
    private bool markTheQuestComplete;
    private bool shouldMarkQuest;

    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = dialogSentences[currentSentence];
        instacne = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1")) //skip dialog when pressing on the mouse
            {
                if (!dialogJustStarted) 
                {
                    currentSentence++;
                    if (currentSentence >= dialogSentences.Length)
                    {
                        dialogBox.SetActive(false);
                        GameManager.instance.dialogBoxOpned = false;

                        if (shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if (markTheQuestComplete)
                            {
                                QuestManager.instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.instance.MarkQuestInComplete(questToMark);

                            }
                        }
                    }
                    else
                    {
                        CheckForName();
                        dialogText.text = dialogSentences[currentSentence];
                    }
                }
                else
                {
                    dialogJustStarted = false;
                }
                


            }
        }
    }

    public void ActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markTheQuestComplete = markComplete;
        shouldMarkQuest = true;

            
    }

    public void ActivateDialog(string[] newSentencesToUse)
    {
        dialogSentences = newSentencesToUse;
        currentSentence = 0;
        CheckForName();
        dialogText.text = dialogSentences[currentSentence]; //first sentence of dialog showed here
        dialogBox.SetActive(true);
        dialogJustStarted = true;
        GameManager.instance.dialogBoxOpned = true;
    }
    public bool IsDialogBoxActive()
    {
        return dialogBox.activeInHierarchy;
    }
    
    void CheckForName()
    {
        if (dialogSentences[currentSentence].StartsWith("#"))
        {
            nameText.text = dialogSentences[currentSentence].Replace("#", "");
            currentSentence++;
        }
    }
}
