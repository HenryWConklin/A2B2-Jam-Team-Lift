using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoEnemy : MonoBehaviour, IDamagable {
    /// Every time the enemy moves, it moves by this step
    public Vector2 stepSize = new Vector2(-0.64f, 0);
    private Rigidbody2D rigidbody2d;
    
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        bool flip = Random.value > 0.5;
        if (flip) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        int rotate = Random.Range(0,4);
        transform.rotation = Quaternion.Euler(0, 0, rotate * 90.0f);
    }

    public void Step() {
        rigidbody2d.MovePosition(rigidbody2d.position + stepSize);
    }

    private void OnDeath() {
        Destroy(gameObject);
    }

    void IDamagable.Damage(float damage) {
        // Die on any damage for now
        OnDeath();
    }
}