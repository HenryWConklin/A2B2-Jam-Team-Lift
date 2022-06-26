using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameStarted;

    public AudioSource currentSong;
    public AudioSource glitchedSong;
    public AudioClip[] mainMenuThemes;
    public AudioClip[] gameSongs;
    public AudioClip[] glitchedGameSongs;
    public int songIndex; 

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying GameManager");
            Destroy(this.gameObject);
        }
        else
            Instance = this;

        SetToMainMenu();
    }

    public void SetToMainMenu()
    {
        currentSong.clip = mainMenuThemes[UnityEngine.Random.Range(0, mainMenuThemes.Length)];
        currentSong.Play();
    }

    public void StartGame()
    {
        songIndex = UnityEngine.Random.Range(0, gameSongs.Length);
        currentSong.clip = gameSongs[songIndex];
        glitchedSong.clip = glitchedGameSongs[songIndex];
        glitchedSong.volume = 0;
        
        currentSong.Play();
        glitchedSong.Play();

    }
    
}
