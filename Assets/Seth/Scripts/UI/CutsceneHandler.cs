using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{
    public static CutsceneHandler Instance;
    public IEnumerator EndCutscene_Holder;
    public AudioSource musicAudioSource; 

    public Image blackScreenImage; 
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying CutsceneHandler");
            Destroy(this.gameObject);
        }
        else
            Instance = this;
    }

    void Start()
    {
        musicAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Cutscene Skipped");
            EndCutscene();
            
        }
        
    }


    public void EndCutscene()
    {
        if (EndCutscene_Holder == null)
        {
            EndCutscene_Holder = EndCutscene_Co();
            StartCoroutine(EndCutscene_Holder);

        }
        else
        {
            return;
        }
    }

    public IEnumerator EndCutscene_Co()
    {

        float elapsedTime = 0f;
        float timeToWait = 1f;
        Color originalColor = blackScreenImage.color;
        

        while (elapsedTime <= timeToWait)
        {
            elapsedTime += Time.deltaTime;
            blackScreenImage.color = new Color(originalColor.r, originalColor.g, originalColor.b,
                Mathf.Lerp(0, 1, elapsedTime / timeToWait));

            musicAudioSource.volume = Mathf.Lerp(0.6f, 0, elapsedTime / timeToWait);
            yield return null;

        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("PlayZone");


    }
}
