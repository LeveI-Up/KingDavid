using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestPanel : MonoBehaviour
{

    public static QuestPanel instance;

    [SerializeField] TextMeshProUGUI questText;
    [SerializeField] TextMeshProUGUI mapNameText;
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
                for(int i=1;i< QuestManager.instance.GetQuestNames().Length; i++)
                {
                    if (QuestManager.instance.GetQuestMarkersCompleted()[i]==false)
                    {
                        questText.text = QuestManager.instance.GetQuestNames()[i];
                        break;
                    }
                }
                questScreen.SetActive(true);
            }
        }
        mapNameText.text = SceneManager.GetSceneAt(1).name;
    }
    public void CloseQuestScreen()
    {
        questScreen.SetActive(false);
    }


}