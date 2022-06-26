using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ShootHandler shootHandler;
    public Camera gameCam;
    public int maxHealth = 3;
    public int health;
    public bool isDead;
    public bool invulFramesActive;

    private SpriteRenderer spriteRenderer;
    private Material startMaterial;
    public Material flashMaterial;
    public GameObject explosionParticle;

    [Header("Sound Settings")]
    public AudioSource deathLaughAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource corruptBoomAudioSource;
    public float maxGlitchIntensity = 0.3f;

    private GlitchShader glitchShader;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        shootHandler = GetComponent<ShootHandler>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        gameCam = Camera.main;
        startMaterial = spriteRenderer.material;
        health = maxHealth;
        glitchShader = gameCam.GetComponent<GlitchShader>();

    }


    public void TakeDamage()
    {
        if (invulFramesActive || health <= 0)
            return;

        health -= 1;
        glitchShader.intensity = (1.0f - ((float)health / maxHealth)) * maxGlitchIntensity;
        StartCoroutine(InvulFrameTimer());
        if (health <= 0)
        {
            Die();
        }
    }


    private IEnumerator InvulFrameTimer()
    {
        invulFramesActive = true;
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.material = startMaterial;
        invulFramesActive = false;
    }

    private void Die()
    {
        isDead = true;
        Destroy(GetComponent<BoxCollider2D>());
        StartCoroutine(Die_Co());
    }


    public IEnumerator Die_Co()
    {
        float elapsedTime = 0f;
        float timeToWait = 1f;


        corruptBoomAudioSource.Play();
        Instantiate(explosionParticle, transform.position, quaternion.identity);

        while (elapsedTime <= timeToWait)
        {
            elapsedTime += Time.deltaTime;
            GameManager.Instance.currentSong.volume = Mathf.Lerp(1, 0, (elapsedTime / timeToWait));
            yield return null;

        }

        yield return new WaitForSeconds(2f);
        elapsedTime = 0f;
        while (elapsedTime <= timeToWait)
        {
            elapsedTime += Time.deltaTime;
            GameManager.Instance.currentSong.volume = Mathf.Lerp(0, 1, (elapsedTime / timeToWait));
            yield return null;

        }
        deathLaughAudioSource.Play();
        UIManager.Instance.DisplayGameOverScreen();

    }

}
