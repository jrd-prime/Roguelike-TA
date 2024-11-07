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
                // rounded corners clipping
                float2 coords = i.uv.xy;

                coords.x *= 8;
                // return float4(coords, 0, 1);
                
                float2 poitOnLineSeg = float2(clamp(coords.x, .5, 7.5), .5);


                float sdf = distance(coords, poitOnLineSeg) * 2 - 1;
                

                

                clip(-sdf);


                float borederSdf = sdf + .1;
                return float4(borederSdf,0 , 0, 1);

                 // return float4(borederSdf.xxx,  1);

                float pd = fwidth(borederSdf); // screen space partial derivative

                 // return float4(pd.xxx, 1);

                float borderMask = 1 - saturate(borederSdf / pd);
                // float borderMask = step(0, -borederSdf);
                // return float4(borderMask.xxx, 1);
                // return float4(borederSdf.xxx, 1);


                

                float3 healthBarColor = lerp(_ColorMin, _ColorMax, _Health);

// return float4(healthBarColor,1);
                float healthBarMask = _Health > i.uv.x;

                return float4(lerp(_ColorBg, healthBarColor, healthBarMask) * borderMask, 0);
                // return float4(healthBarColor, healthBarMask);
            }
            ENDCG
        }
    }
}