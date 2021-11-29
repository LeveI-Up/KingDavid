using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource[] backgraoundMusic, SFX;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        //singelton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayBackgroundMusic(5);
        }
        
        
    }

    public void PlaySFX(int soundToPlay)
    {
        if(soundToPlay < SFX.Length)
        {
            SFX[soundToPlay].Play();
        }
    }

    public void PlayBackgroundMusic(int musicToPlay)
    {
        StopMusic();
        if (musicToPlay < backgraoundMusic.Length)
        {
            backgraoundMusic[musicToPlay].Play();
        }
    }
    private void StopMusic()
    {
        foreach(AudioSource music in backgraoundMusic)
        {
            music.Stop();
        }
    }
}
