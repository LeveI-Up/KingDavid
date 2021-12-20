using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{

    public static CamController instance;

    [SerializeField] Player playerTarget;
    [SerializeField] GameObject player;
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField] int musicToPlay;
    
    private bool musicAlreadyPlaying;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerTarget = FindObjectOfType<Player>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = playerTarget.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicAlreadyPlaying)
        {
            musicAlreadyPlaying = true;
            AudioManager.instance.PlayBackgroundMusic(musicToPlay);
        }
        while(playerTarget == null)
        {
            playerTarget = FindObjectOfType<Player>();
            if (virtualCamera)
                virtualCamera.Follow = player.transform;
            
        }
    }

    public int GetmusicToPlay()
    {
        return musicToPlay;
    }

}
