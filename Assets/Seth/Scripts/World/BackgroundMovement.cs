using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
public float backgroundSpeed;
public Renderer backgroundRenderer;
public bool isVertical;

    void Update()
    {
        backgroundRenderer.material.mainTextureOffset +=  isVertical ? new Vector2(0f, backgroundSpeed * Time.deltaTime)  : new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}
