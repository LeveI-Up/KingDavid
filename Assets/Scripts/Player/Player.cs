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
    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;

    [SerializeField] bool deactivatedMovement = false;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //player movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        if (deactivatedMovement)
        {
            playerRigidBody.velocity = Vector2.zero;
        }
        else
        {
            playerRigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * movespeed;
        }
        

        playerAnimator.SetFloat("movementX", playerRigidBody.velocity.x);
        playerAnimator.SetFloat("movementY", playerRigidBody.velocity.y);

        if(horizontalMovement == move || horizontalMovement == -move || verticalMovement == move || verticalMovement == -move)
        {
            if(!deactivatedMovement)
            playerAnimator.SetFloat("lastX", horizontalMovement);
            playerAnimator.SetFloat("lastY", verticalMovement);

        }
        //limits for the camera
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftEdge.x, topRightEdge.x),
            Mathf.Clamp(transform.position.y, bottomLeftEdge.y, topRightEdge.y),
            Mathf.Clamp(transform.position.z, bottomLeftEdge.z, topRightEdge.z)
        );

    }

    //set camera limits
    public void SetLimit(Vector3 bottomEdgeToSet,Vector3 topEdgeToSet)
    {
        bottomLeftEdge = bottomEdgeToSet;
        topRightEdge = topEdgeToSet;
    }

    //deactive player movement while he is in dialog
    public void DeactiveMovement(bool b)
    {
        deactivatedMovement = b;
    }

    public bool GetDeactiveMovement()
    {
        return deactivatedMovement;
    }
}
