using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : MonoBehaviour
{

    public string arrivalStation;

    // Start is called before the first frame update
    void Start()
    {
        if (arrivalStation == Player.instance.transitionName)
        {
            Player.instance.transform.position = transform.position;
        }

    }


}
