using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class BackgroundTileMovement : MonoBehaviour
{
    public RawImage backgroundImage;
    public float moveSpeed; 


private void Awake()
{
    backgroundImage = GetComponent<RawImage>();
    
}

private void Update()
{
    float newValue = backgroundImage.uvRect.y + (Time.deltaTime * moveSpeed);
    backgroundImage.uvRect = new Rect(0, newValue, 1, 1);
}
}
