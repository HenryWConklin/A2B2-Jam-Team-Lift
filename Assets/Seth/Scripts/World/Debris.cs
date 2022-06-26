using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, IDamagable
{
    public float health;
    public float velocity;
    public float rotationSpeed;
    public Vector2 travelDirection; 
    public Sprite[] debrisSprites;
    public AudioClip[] explosionSounds;
    public GameObject[] explosionParticles;
    public Material flashMaterial;
    public Material normalMaterial;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public void Init(Vector2 _travelDirection)
    {
        travelDirection = _travelDirection - (Vector2)transform.position;
        
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = debrisSprites[UnityEngine.Random.Range(0, debrisSprites.Length)];
        normalMaterial = spriteRenderer.material;
        rotationSpeed = UnityEngine.Random.Range(300, 600);
        GameObject.Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Time.deltaTime * rotationSpeed;
        transform.Rotate(0f,0f,angle);
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(travelDirection.x, travelDirection.y,0f ) * (Time.deltaTime * velocity);
    }

    public void Damage(float damage)
    {
        if (!(health > 0)) return;
        health -= damage;
        StartCoroutine(TakeDamageFX());

    }


    public IEnumerator TakeDamageFX()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = normalMaterial;
        if (health <= 0)
        {
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
            var player = col.gameObject.GetComponent<PlayerBase>();
            if (player.isDead)
                return;
            player.TakeDamage();
            Explode();
        }
    }
}
