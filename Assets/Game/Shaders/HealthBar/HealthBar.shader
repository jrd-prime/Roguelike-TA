Shader "Unlit/HealthBar"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        _ColorMin ("Color Min", Color) = (1,1,1,1)
        _ColorMax ("Color Max", Color) = (1,1,1,1)
        _ColorBg ("Color Bg", Color) = (1,1,1,1)
        _Health ("Health", Range(0.0,1.0)) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            //            "RenderType"="Transparent"
            //            "Queue"="Transparent"
        }
        Pass
        {
            //            ZWrite Off
            //            Blend SrcAlpha OneMinusSrcAlpha // alpha blend

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Health;
            float4 _ColorMin;
            float4 _ColorMax;
            float4 _ColorBg;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };


            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            Interpolators vert(MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(Interpolators i) : SV_Target
            {
                float3 healthBarColor = lerp(_ColorMin, _ColorMax, _Health);


                float healthBarMask = _Health > i.uv.x;

                return float4(lerp(_ColorBg, healthBarColor, healthBarMask), 0);
                // return float4(healthBarColor, healthBarMask);
            }
            ENDCG
        }
    }
}