﻿using UnityEngine;

public class Follow : MonoBehaviour
{
    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField] Animator AnimalAnimation;
    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    // Use this for initialization
    private void Start()
    {
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Move Enemy
        Move();
    }

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            if(transform.position.y < waypoints[waypointIndex].transform.position.y)
            {
                AnimalAnimation.SetBool("FacingDown", false); 
            }
            else if(transform.position.y > waypoints[waypointIndex].transform.position.y)
            {
                AnimalAnimation.SetBool("FacingDown", true);
            }
            else if (transform.position.x > waypoints[waypointIndex].transform.position.x)
            {
                AnimalAnimation.SetBool("FacingRight", false);
            }
            else if (transform.position.x < waypoints[waypointIndex].transform.position.x)
            {
                AnimalAnimation.SetBool("FacingRight", true);
            }
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);
            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if ((transform.position.x == waypoints[waypointIndex].transform.position.x) && (transform.position.y == waypoints[waypointIndex].transform.position.y))
            {
                waypointIndex += 1;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}
