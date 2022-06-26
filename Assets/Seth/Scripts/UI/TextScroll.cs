using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    public float scrollSpeed;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * scrollSpeed, transform.position.z);
        if (transform.position.y >= 700)
        {
            CutsceneHandler.Instance.EndCutscene();
        }
    }
}
