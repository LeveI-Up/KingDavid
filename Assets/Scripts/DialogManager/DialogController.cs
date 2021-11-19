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
                currentSentence++;
                if (currentSentence >= dialogSentences.Length) 
                {
                    dialogBox.SetActive(false);
                }
                else
                {
                    dialogText.text = dialogSentences[currentSentence];

                }


            }
        }
    }
}
