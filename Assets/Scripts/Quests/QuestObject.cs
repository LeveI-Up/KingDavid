using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{

    [SerializeField] GameObject[] objectToActivate;
    [SerializeField] string[] questToCheck;
    [SerializeField] bool activateIfComplete;

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
    }
}
