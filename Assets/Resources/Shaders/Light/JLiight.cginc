#include  "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"
#include "UnityLightingCommon.cginc"

#define USE_LIGHTING

struct MeshData
{
    float4 vertex : POSITION;
    float4 normal : NORMAL;
    float2 uv : TEXCOORD0;
};

struct Interpolators
{
    float4 vertex : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : TEXCOORD1;
    float3 wPosition : TEXCOORD2;
    LIGHTING_COORDS(3, 4)
};

sampler2D _MainTex;
float4 _MainTex_ST;
float _Gloss;
float4 _Color;

Interpolators vert(MeshData v)
{
    Interpolators o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.normal = UnityObjectToWorldNormal(v.normal);
    o.wPosition = mul(unity_ObjectToWorld, v.vertex);
    TRANSFER_VERTEX_TO_FRAGMENT(o); // lighting
    return o;
}

float4 frag(Interpolators i) : SV_Target
{
    #ifdef  USE_LIGHTING
    // diffuse lightning
    float3 N = normalize(i.normal);
    float3 L = normalize(UnityWorldSpaceLightDir(i.wPosition));
    float attenuation = LIGHT_ATTENUATION(i);
    float3 lambert = saturate(dot(N, L));
    float3 diffuseLight = lambert * _LightColor0.xyz;

    // specular lightning
    float3 V = normalize(_WorldSpaceCameraPos - i.wPosition);
    float3 H = normalize(L + V);
    // float3 R = reflect(-L, N); // uses foe Phong
    float3 specularLight = saturate(dot(H, N)) * (lambert > 0); // Blinn-Phong

    float specularExponent = exp2(_Gloss * 11) + 2;
    specularLight = pow(specularLight, specularExponent) * _Gloss * attenuation;
    specularLight *= _LightColor0.xyz;

    return float4(diffuseLight * _Color + specularLight, 1);
    #else
    #ifdef IS_IN_BASE_PASS
            return _Color;
    #else
    return 0;
    #endif
    #endif
}
