using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMove : MonoBehaviour
{
    public static float mSpeed = 5f;

   

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.y -= mSpeed * Time.deltaTime ;
        transform.position = pos;
        //Debug.Log(mSpeed);
    }
    
}
