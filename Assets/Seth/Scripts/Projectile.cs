using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 travelDirection;
    public float velocity; 

    public void Init(Vector3 _travelDirection)
    {
        travelDirection = _travelDirection;
        transform.eulerAngles = new Vector3(0f, 0f,GetAngleFromVectorFloat(travelDirection) );
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(travelDirection.x, travelDirection.y,0f ) * (Time.deltaTime * velocity);
    }


    protected float GetAngleFromVectorFloat(Vector3 dir)
    {
        //dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n < 0 ) n+= 360;
        return n;
    }
}
