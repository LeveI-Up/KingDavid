using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class LevelManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;
    private float yLimit = 0.5f;
    private float maxxLimit = 0.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        //set the camera limits to the player
        bottomLeftEdge = tilemap.localBounds.min + new Vector3(yLimit, yLimit, 0f);
        topRightEdge = tilemap.localBounds.max + new Vector3(-maxxLimit, -yLimit, 0f);
        Player.instance.SetLimit(bottomLeftEdge, topRightEdge);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
