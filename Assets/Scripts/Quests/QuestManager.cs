using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager instance;

    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkersCompleted;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkersCompleted = new bool[questNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Saved");
            SaveQuestData();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Loaded");
            LoadQuestData();
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i=0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }
        Debug.LogWarning("Quest " + questToFind +" does no exist");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);
        if(questNumberToCheck != 0)
        {
            return questMarkersCompleted[questNumberToCheck];
        }
        return false;
    }

    public void UpdateQuestObjects()
    {
        QuestObject[] questObjects = FindObjectsOfType<QuestObject>();
        if (questObjects.Length > 0)
        {
            foreach (QuestObject questObject in questObjects)
            {
                questObject.CheckForCompletion();
            }
        }
    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = true;

        UpdateQuestObjects();
    }

    public void MarkQuestInComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = false;

        UpdateQuestObjects();

    }

    public void SaveQuestData()
    {
        for(int i=0; i< questNames.Length; i++)
        {
            if (questMarkersCompleted[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 0);
            }
        }
    }

    /*This method checks if the player have saved data of his active quests.
     * If he have the method load the data from the saved quests.
     * else do nothing.
     * */
    public void LoadQuestData()
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            int valueToSet = 0;
            if(PlayerPrefs.HasKey("QuestMarker_" + questNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questNames[i]);
            }
            if (valueToSet == 0)
            {
                questMarkersCompleted[i] = false;
            }
            else
            {
                questMarkersCompleted[i] = true;
            }
        }

    }

}
