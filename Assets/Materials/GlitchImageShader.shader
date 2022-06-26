Shader "GlitchImageShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FontTex ("Font texture", 2D) = "black" {}
        _CellSize ("Cell size", Vector) = (.1, .1, 0, 0)
        _Intensity("Intensity", Float) = 0.3
        _Seed("Seed", Float) = 1.0
        _GlitchAlpha("Glitch alpha", Float) = 0.2
        _AspectRatio("Aspect ratio", Float) = 1.0
        _FontScale("Font scale", Float) = 2 
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _FontTex;
            float4 _CellSize;
            float _Intensity;
            float _Seed;
            float _GlitchAlpha;
            float _AspectRatio;
            float _FontScale;

            float random( float2 p )
            {
                float2 K1 = float2(
                23.14069263277926, // e^pi (Gelfond's constant)
                2.665144142690225 // 2^sqrt(2) (Gelfondâ€“Schneider constant)
                );
                return frac( cos( dot(p,K1) ) * 12345.6789 );
            }
            fixed4 frag (v2f i) : SV_Target
            {
                float2 cell;
                float2 cellSize = _CellSize.xy * float2(_AspectRatio, 1.0);
                float2 frac = modf(i.uv / cellSize, cell);
                float2 cellSeed = cell * _Seed;
                bool glitchCell = random(cellSeed) < _Intensity;
                float4 overlayColor = float4(random(cellSeed * 2), random(cellSeed * 3), random(cellSeed*4), 1.0);
                float2 offset = float2(random(cellSeed * 5), random(cellSeed*6)) * 2 - 1;
                float4 offsetColor = tex2D(_MainTex, i.uv + offset);
                float noiseVal = step(random(i.uv), 0.5);
                float4 noiseColor = float4(random(i.uv), random(i.uv*2), random(i.uv*3), 1.0);
                float2 fontNoiseUV = float2(random(cellSeed * 8), random(cellSeed * 9));
                float4 fontNoise = tex2D(_FontTex, i.uv * _FontScale);

                fixed4 col = tex2D(_MainTex, i.uv);

                float type = random(cellSeed* 7);
                float4 glitchOverlay = overlayColor * col;
                glitchOverlay = (type > 0.5 && type <= 0.8) ? (fontNoise + col * (1.0 - fontNoise)) : glitchOverlay;
                glitchOverlay = (type > 0.8 && type <= 0.9) ? offsetColor : glitchOverlay;
                glitchOverlay = (type > 0.9) ? noiseColor : glitchOverlay;

                col = glitchCell ? ((col * (1.0 - _GlitchAlpha)) + (glitchOverlay * (_GlitchAlpha))) : col;
                return col;
            }
            ENDCG
        }
    }
}
