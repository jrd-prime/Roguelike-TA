Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "LightMode" = "Always"
        }
        CGPROGRAM
        #pragma surface surf Unlit
        struct Input
        {
        };

        void surf(Input IN)
        {
        }
        ENDCG

    }
}