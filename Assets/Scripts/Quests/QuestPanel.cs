using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestPanel : MonoBehaviour
{

    public static QuestPanel instance;

    [SerializeField] TextMeshProUGUI questText;
    [SerializeField] GameObject questScreen;


    [SerializeField] bool markQuestComplete;
    [SerializeField] string questToMarkName;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (questScreen.activeInHierarchy)
            {
                questScreen.SetActive(false);
            }
            else
            {
                questText.text = "ילש םייחב יתשגפש לודג יכה הנוז ןבה התא הירוא";
                questScreen.SetActive(true);
            }
        }

    }
    public void CloseQuestScreen()
    {
        questScreen.SetActive(false);
    }


}