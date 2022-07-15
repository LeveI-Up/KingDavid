using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWonManager : MonoBehaviour
{

    private bool loaded, unloaded;

    // Start is called before the first frame update
    void Start()
    {
        
        //Player.instance.gameObject.SetActive(false);
        //MenuManager.instance.gameObject.SetActive(false);
        BattleManager.instance.gameObject.SetActive(false);
        //AudioManager.instance.PlayBackgroundMusic(9);
        GameManager.instance.battleIsActive = false;
        BattleManager.instance.SetIsBattleActive(false);
        Player.instance.DeactiveMovement(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("the scene is " + PlayerPrefs.GetString("Current_Scene"));
        }
    }

    public void QuitToMainMenu()
    {
        if (!loaded)
        {
            Player.instance.transitionName = "MainMenu";

            MenuManager.instance.FadeImage();

            StartCoroutine(LoadSceneCoroutine());



            loaded = true;
        }
    }
    public void LoadLast
        ()
    {
        DestroyGameSession();
        if (!loaded)
        {
            Player.instance.transitionName = PlayerPrefs.GetString("Current_Scene");

            MenuManager.instance.FadeImage();

            StartCoroutine(LoadSavedSceneCoroutine());



            loaded = true;
        }
    }

    public void ExitButton()
    {
        DestroyGameSession();
        Application.Quit();
        Debug.Log("Quit the game");

    }
    private static void DestroyGameSession()
    {

    }
    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        MenuManager.instance.FadeOut();
        

        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene(SceneManager.GetSceneAt(1).name);
            //Debug.Log("Scene unload called: " + arrivingFrom);
        }

    }
    IEnumerator LoadSavedSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Current_Scene"), LoadSceneMode.Additive);


        GameManager.instance.LoadData();
        MenuManager.instance.FadeOut();
        

        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene(SceneManager.GetSceneAt(1).name);
            //Debug.Log("Scene unload called: " + arrivingFrom);
        }

    }


    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(0.5f);

        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene(SceneManager.GetSceneAt(1).name);
            Debug.Log("Scene unload called: " + SceneManager.GetSceneAt(1));
        }
    }

    private static void DestroyGame()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(Player.instance.gameObject);
        Destroy(MenuManager.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);
    }
}
