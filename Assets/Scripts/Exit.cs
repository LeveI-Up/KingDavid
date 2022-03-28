using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Exit : MonoBehaviour
{
    
    [SerializeField] string sceneToLoad;
    [SerializeField] string goingTo;
    [SerializeField] string arrivingFrom;
    


    bool loaded;
    bool unloaded;




    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (!loaded)
            {
                Player.instance.transitionName = goingTo;

                MenuManager.instance.FadeImage();

                StartCoroutine(LoadSceneCoroutine());

                Debug.Log("Scene load called: " + sceneToLoad);

                loaded = true;

                /*if(sceneToLoad == "MyMap")
                {
                    GameManager.instance.sceneObjects[0].gameObject.SetActive(true);
                }
                else
                {
                    GameManager.instance.sceneObjects[0].gameObject.SetActive(false);
                }*/
            }

        }

    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        MenuManager.instance.FadeOut();

        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene(arrivingFrom);
            Debug.Log("Scene unload called: " + arrivingFrom);
        }

    }


    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(0.5f);

        if (!unloaded)
        {
            unloaded = true;
            AnyManager.anyManager.UnloadScene(arrivingFrom);
            Debug.Log("Scene unload called: " + arrivingFrom);
        }
    }

    public void ActiveScene()
    {
        arrivingFrom = SceneManager.GetActiveScene().name;
    }
}
