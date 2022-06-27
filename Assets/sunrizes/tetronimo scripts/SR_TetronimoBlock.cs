using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_TetronimoBlock : MonoBehaviour, IDamagable
{
    public int unitMove;
    public float health = 3; 

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public GameObject[] explosionParticles;
    public Sprite[] baseSprites;
    public Material flashMaterial;
    private Material normalMaterial;
    public AudioSource hitAudioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        normalMaterial = spriteRenderer.material;
        spriteRenderer.sprite = baseSprites[UnityEngine.Random.Range(0, baseSprites.Length)];
        StartCoroutine(BlockDrop());
    }

    IEnumerator BlockDrop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y - unitMove));
        }
    }

    public void Damage(float damage)
    {
        
            if (!(health > 0)) return;
            health -= damage;
            hitAudioSource.Play();
            StartCoroutine(TakeDamageFX());

    }
    public IEnumerator TakeDamageFX()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = normalMaterial;
        if (health <= 0)
        {
            GameManager.Instance.UpdateScore(500f);
            Explode(); 
        }
    }

    private void Explode()
    {
        

        GameObject newParticle = Instantiate(explosionParticles[UnityEngine.Random.Range(0, explosionParticles.Length)],
            transform.position, Quaternion.identity);
        
        Destroy(newParticle, 2f);
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Explode();
            col.gameObject.GetComponent<PlayerBase>().TakeDamage();
        }
    }
}