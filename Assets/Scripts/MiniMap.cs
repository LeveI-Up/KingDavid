using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        
        newPos.z = -15;
        transform.position = newPos;
    }

}
