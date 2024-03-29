﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] string transitionAreaName;
    [SerializeField] AreaEnter theAreaEnter;
    private float sec = 1f;
    // Start is called before the first frame update
    void Start()
    {
        theAreaEnter.transitionAreaName = transitionAreaName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.transitionName = transitionAreaName;
            MenuManager.instance.FadeImage();
            StartCoroutine(LoadSceneCoroutine());
        }
    }
    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(sceneToLoad);


    }
}
