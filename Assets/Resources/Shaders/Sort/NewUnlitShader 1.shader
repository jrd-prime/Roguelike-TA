Shader "Unlit/J/Vertex Offset"
{
    Properties
    {
        //        _MainTex ("Text",2D) = "white" {}
        //        _Scale ("UV Scale", Float) = 1.0
        _ColorA ("Color A", Color) = (1,1,1,1)
        _ColorB ("Color B", Color) = (1,1,1,1)
        //        _Offset ("UV Offset", Float) = 0
        _ColorStart ("Color Start", Range(0,1)) = 0
        _ColorEnd ("Color End", Range(0,1)) = 1
        _WaveAmp ("Wave Amp", Range(0,.2)) = .1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.2831

            // sampler2D _MainTex;
            // float4 _MainTex_ST;

            float _Value;
            float4 _ColorA;
            float4 _ColorB;
            float _ColorStart;
            float _ColorEnd;
            float _WaveAmp;
            // float _Scale;
            // float _Offset;

            // per-vertex mesh data
            struct meshData
            {
                // vertex position
                float4 vertex : POSITION;
                // normal
                float3 normals : NORMAL;
                // tangent
                // float4 tangent : TANGENT;
                // color
                // float4 color : COLOR;
                // uv coordinates
                float4 uv0 : TEXCOORD0; // diff/normal map textures
                // float2 uv1 : TEXCOORD1; // lightmap coords
            };


            struct Interpolator // v2f, fragmentinput
            {
                float4 vertex : SV_POSITION; // clip space position
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            Interpolator vert(meshData v)
            {
                Interpolator o;

                float wave = cos((v.uv0.y + _Time.y * -.1) * TAU * 5);
                float wave2 = cos((v.uv0.x + _Time.y * -.1) * TAU * 1);

                v.vertex.y = wave * wave2 * _WaveAmp;

                o.vertex = UnityObjectToClipPos(v.vertex); // local space to clip space
                // o.vertex = v.vertex; // screen space??
                o.normal = mul((float3x3)unity_ObjectToWorld, v.normals); //UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv0; //(v.uv0 + _Offset) * _Scale;
                return o;
            }

            float InverseLerp(float a, float b, float v)
            {
                return (v - a) / (b - a);
            }

            fixed4 frag(Interpolator i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);

                // blend between two colors based on the X UV coord

                // float t = saturate(InverseLerp(_ColorStart, _ColorEnd, i.uv.x));
                // t = frac(t);
                // float4 outColor = lerp(_ColorA, _ColorB, t);

                // return float4(0, 1, 0, 1); 
                // return float4(i.uv.xxx , 1);
                // return outColor;


                // float t = abs(frac(i.uv.x * 5) * 2 - 1);


                // return float4(i.uv, 0, 1);

                float yOffset = cos(i.uv.x * TAU * 5);

                float wave = cos((i.uv.y + _Time.y * -.1) * TAU * 5);
                return wave;

                float topBottomRemover = (abs(i.normal.y) < 0.999);

                float waves = wave * topBottomRemover;


                float4 gradient = lerp(_ColorA, _ColorB, i.uv.y);

                return gradient * waves * 1;
            }
            ENDCG
        }
    }
}