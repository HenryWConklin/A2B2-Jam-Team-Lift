using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPulse : MonoBehaviour
{
    public TMP_Text text;
    public Color originalColor;
    public Color targetColor;
    public float glowTime;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        originalColor = text.color;
    }

    private void Update()
    {
        text.color = Color.Lerp(originalColor, targetColor, Mathf.PingPong(Time.time, glowTime));
    }
}
