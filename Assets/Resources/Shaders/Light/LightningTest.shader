Shader "Unlit/LightningTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Gloss ("Gloss", Range(0, 1)) = 1
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "UniversalMaterialType" = "Lit"
            "IgnoreProjector" = "True"
        }

        // Base pass
        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #define IS_IN_BASE_PASS

            #include "JLiight.cginc"
            ENDHLSL
        }
        // Add pass
        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForwardAdd"
            }

            Blend One One // src * 1 + dst * 1
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd

            #include "JLiight.cginc"
            ENDHLSL
        }
    }
}