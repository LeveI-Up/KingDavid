using UnityEngine;

public class FollowAndDisappear : MonoBehaviour
{
    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private string questToComplete;
    [SerializeField]
    private bool changeAnimation;

    [SerializeField]
    private bool shouldMarkQuest;
    

    [SerializeField] Animator AnimalAnimation;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed ;
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
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            if (changeAnimation)
            {
                if (transform.position.y < waypoints[waypointIndex].transform.position.y)
                {
                    AnimalAnimation.SetBool("FacingDown", false);
                }
                else if (transform.position.y > waypoints[waypointIndex].transform.position.y)
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
            }

            Debug.Log("f");
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
            if (shouldMarkQuest)
            {
                QuestManager.instance.MarkQuestComplete(questToComplete);
            }
        }
    }
}
