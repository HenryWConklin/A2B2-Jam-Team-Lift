using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSFX : MonoBehaviour
{
    public AudioClip[] explosionSounds;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = explosionSounds[UnityEngine.Random.Range(0, explosionSounds.Length)];
        audioSource.Play();

    }
}
