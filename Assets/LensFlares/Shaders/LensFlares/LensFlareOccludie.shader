Shader "+dotsquid/LensFlare/Occludie"
{
    Properties
    {
        _MainTex ("Main", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Occludie" 
            "PreviewType"="Plane"
            "ForceNoShadowCasting"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = float4(0.0, 0.0, 0.0, 0.0);
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                clip(-1.0);
                return fixed4(0.0, 0.0, 0.0, 0.0);
            }
        ENDCG
        }
    }
}