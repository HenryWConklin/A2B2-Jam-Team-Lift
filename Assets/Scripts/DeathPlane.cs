using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public float damage = 1e10f;

    void OnTriggerEnter2D(Collider2D collision) {
        var damageable = collision.gameObject.GetComponent<IDamagable>();
        if (damageable != null) {
            damageable.Damage(damage);
        }
    }
}
