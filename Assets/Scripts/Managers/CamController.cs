using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    [SerializeField] Player playerTarget;
    [SerializeField] GameObject player;
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField] int musicToPlay;
    
    private bool musicAlreadyPlaying;
    // Start is called before the first frame update
    void Start()
    {
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
            playerTarget = player.GetComponent<Player>();
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = playerTarget.transform;
        }
    }
}
