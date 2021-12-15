using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    [SerializeField] string newGameScene;
    [SerializeField] GameObject continueButton;
    bool loaded;
    bool unloaded;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Player_Pos_X"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
        AudioManager.instance.PlayBackgroundMusic(7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameButton()
    {
        if (!loaded)
        {
            Player.instance.transitionName = newGameScene;

            MenuManager.instance.FadeImage();

            StartCoroutine(LoadSceneCoroutine());

            

            loaded = true;
        }
        Player.instance.transform.position = new Vector3(19, 4, 0);
    }
    
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Quit the game");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(newGameScene, LoadSceneMode.Additive);
        MenuManager.instance.FadeImage();
        DialogController.instacne.GetDialogBox().SetActive(true);

        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene("MainMenu");
            //Debug.Log("Scene unload called: " + arrivingFrom);
        }

    }


    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(0.5f);

        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene("MainMenu");
            Debug.Log("Scene unload called: " + "MainMenu");
        }
    }

    /*
    public void ActiveScene()
    {
        arrivingFrom = SceneManager.GetActiveScene().name;
    }
    */

}
