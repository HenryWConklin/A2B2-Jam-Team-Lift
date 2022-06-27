using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public static PlayerBase Instance;
    public ShootHandler shootHandler;
    public Camera gameCam;
    public int maxHealth;
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
    public AudioSource explosionAudioSource;
    public AudioSource hitAudioSource;
    public AudioSource shootAudioSource;
    public float maxGlitchIntensity = 0.3f;

    private GlitchShader glitchShader;


    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying GameManager");
            Destroy(this.gameObject);
        }
        else
            Instance = this;
        
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
        hitAudioSource.Play();
        SR_TetronimoGrid.Instance.CorruptGrid(SR_TetronimoGrid.Instance.CheckGridLines());
        glitchShader.intensity = (1.0f - ((float)health / maxHealth)) * maxGlitchIntensity;
        StartCoroutine(InvulFrameTimer());
        if (health <= 0)
        {
            Die();
        }

        GameManager.Instance.currentSong.volume -= .20f;
        GameManager.Instance.glitchedSong.volume += 0.20f;  
    }


    private IEnumerator InvulFrameTimer()
    {
        invulFramesActive = true;
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.material = startMaterial;
        invulFramesActive = false;
    }

    public void Die()
    {
        isDead = true;
        health = 0;
        Destroy(GetComponent<BoxCollider2D>());
        StartCoroutine(Die_Co());
    }


    public IEnumerator Die_Co()
    {
        float elapsedTime = 0f;
        float timeToWait = 1f;


        corruptBoomAudioSource.Play();
        explosionAudioSource.Play();
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
            GameManager.Instance.glitchedSong.volume = Mathf.Lerp(0, 1, (elapsedTime / timeToWait));
            yield return null;

        }
        deathLaughAudioSource.Play();
        UIManager.Instance.DisplayGameOverScreen();

    }

}
