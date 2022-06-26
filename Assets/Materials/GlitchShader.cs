using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchShader : MonoBehaviour
{
    public float intensity = 0.1f;
    public float seed = 1.0f;
    public float glitchAlpha = 1.0f;
    public Vector2 cellSize = new Vector2(0.1f, 0.1f);
    public float scramblePeriod = 0.5f;
    public Texture fontTexture;
    public Material material;

    void Start()
    {
        material.SetTexture("_FontTex", fontTexture);
        InvokeRepeating("RandomizeSeed", 0.0f, scramblePeriod);
    }
    void RandomizeSeed()
    {
        seed = Random.value;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        material.SetVector("_CellSize", new Vector4(cellSize.x, cellSize.y));
        material.SetFloat("_Seed", seed);
        material.SetFloat("_Intensity", intensity);
        material.SetFloat("_GlitchAlpha", glitchAlpha);
        float aspect = (float)src.height / src.width;
        material.SetFloat("_AspectRatio", aspect);
        Graphics.Blit(src, dst, material);
    }
}
