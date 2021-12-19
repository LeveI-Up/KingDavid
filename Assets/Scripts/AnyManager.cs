using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyManager : MonoBehaviour
{

    public static AnyManager anyManager;


    bool gameStart;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BattleManager.instance.StartBattle(new string[] { "Mage Master", "Blueface", "Mage", "Warlock" });
            BattleManager.instance.gameObject.SetActive(true);
            
        }
    }

    void Awake()
    {
        if (!gameStart)
        {
            anyManager = this;

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            gameStart = true;

        }
    }

    public void UnloadScene(string scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(string scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);

    }
}