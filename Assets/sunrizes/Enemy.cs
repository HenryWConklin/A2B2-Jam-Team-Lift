using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float health;

    void IDamagable.Damage(float damage)
    {
        health -= damage;
    }
}
