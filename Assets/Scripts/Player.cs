using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private int move = 1;
    [SerializeField] int movespeed=1;
    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] Animator playerAnimator;
    public string transitionName;
    // Start is called before the first frame update
    void Start()
    {
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
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        playerRigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * movespeed;

        playerAnimator.SetFloat("movementX", playerRigidBody.velocity.x);
        playerAnimator.SetFloat("movementY", playerRigidBody.velocity.y);

        if(horizontalMovement == move || horizontalMovement == -move || verticalMovement == move || verticalMovement == -move)
        {
            playerAnimator.SetFloat("lastX", horizontalMovement);
            playerAnimator.SetFloat("lastY", verticalMovement);

        }


    }
}
