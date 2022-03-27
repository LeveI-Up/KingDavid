using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{

    [SerializeField] GameObject[] objectToActivate;
    [SerializeField] string[] questToCheck;
    [SerializeField] GameObject[] objectToUnActivate;
    [SerializeField] string[] questToCheckForUnActivate;
    [SerializeField] bool activateIfComplete;
    [SerializeField] bool unActivateIfComplete;


    private void Update()
    {
            CheckForCompletion();
    }
    public void CheckForCompletion()
    {
        for (int i = 0; i < questToCheck.Length; i++)
        {
            if (QuestManager.instance.CheckIfComplete(questToCheck[i]))
            {
                objectToActivate[i].SetActive(activateIfComplete);
            }
        }
        for (int i = 0; i < objectToUnActivate.Length; i++)
        {
            if (QuestManager.instance.CheckIfComplete(questToCheckForUnActivate[i]))
            {
                objectToUnActivate[i].SetActive(unActivateIfComplete);
            }
        }
    }
}
