using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    [SerializeField] string newGameScene;
    [SerializeField] GameObject continueButton;
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
        SceneManager.LoadScene(newGameScene);
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

 
}
