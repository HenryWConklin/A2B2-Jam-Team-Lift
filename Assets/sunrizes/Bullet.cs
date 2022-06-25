using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damageAmount;

    private void Start()
    {
        Invoke("DestroyBullet", 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        
        if (damagable != null)
            damagable.Damage(damageAmount);

        Destroy(gameObject);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}