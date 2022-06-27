using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 travelDirection;
    public float velocity;
    private bool passed;
    public GameObject projectileHitParticle;

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Damagable") || col.CompareTag("tetroblock") && !passed )
        {
            passed = true;
            Debug.Log("Hit something, dealing damage");
            col.GetComponent<IDamagable>().Damage(1f);

            GameObject newParticle = Instantiate(projectileHitParticle, transform.position, quaternion.identity);
            Destroy(newParticle, 2f);
            Destroy(gameObject);
        }
        if (col.CompareTag("lockedBlock"))
        {
            GameObject newParticle = Instantiate(projectileHitParticle, transform.position, quaternion.identity);
            Destroy(newParticle, 2f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damagable"))
        {
            Debug.Log("Hit something, dealing damage");
            other.GetComponent<IDamagable>().Damage(1f);
        }


    }
}
