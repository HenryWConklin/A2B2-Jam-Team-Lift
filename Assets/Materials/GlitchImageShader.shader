Shader "GlitchImageShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CellSize ("Cell size", Vector) = (.1, .1, 0, 0)
        _Intensity("Intensity", Float) = 0.3
        _Seed("Seed", Float) = 1.0
        _GlitchAlpha("Glitch alpha", Float) = 0.2
        _AspectRatio("Aspect ratio", Float) = 1.0
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
            float4 _CellSize;
            float _Intensity;
            float _Seed;
            float _GlitchAlpha;
            float _AspectRatio;

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

                float type = random(cellSeed* 7);
                float4 glitchOverlay = overlayColor;
                glitchOverlay = (type > 0.5) ? glitchOverlay : offsetColor;
                
                fixed4 col = tex2D(_MainTex, i.uv);
                col = glitchCell ? ((col * (1.0 - _GlitchAlpha)) + (glitchOverlay * (_GlitchAlpha))) : col;
                return col;
            }
            ENDCG
        }
    }
}
