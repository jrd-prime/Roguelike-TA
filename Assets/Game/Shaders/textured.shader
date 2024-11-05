Shader "Unlit/textured"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Pattern ("Texture", 2D) = "white" {}
        _Rock ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define  TAU 6.2831
            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _Pattern;
            sampler2D _Rock;
            float4 _MainTex_ST; //optional

            Interpolators vert(MeshData v)
            {
                Interpolators o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); // obj to world
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float GetWave(float coord)
            {
                float wave = cos((coord - _Time.y * .1) * TAU * 5) * .5 + .5;
                wave *= 1 - coord;
                return wave;
            }

            fixed4 frag(Interpolators i) : SV_Target
            {
                float2 topDownProjection = i.worldPos.xz;
                float4 moss = tex2D(_MainTex, topDownProjection);
                float4 rock = tex2D(_Rock, topDownProjection);

                float pattern = 1-tex2D(_Pattern, i.uv).x;
                // float p = GetWave(pattern);

                float4 finalColor = lerp(rock, moss, pattern);
                return finalColor;

                // return float4(topDownProjection, 0, 1);

                // return float4(i.worldPos.xyz, 1);
                fixed4 col = tex2D(_MainTex, topDownProjection);
                return col;
            }
            ENDCG
        }
    }
}