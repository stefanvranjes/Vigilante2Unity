Shader "+dotsquid/LensFlare/Occlusion"
{
    Properties
    {
        _MainTex ("Main", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        { 
            "Queue" = "Geometry"
            "IgnoreProjector"="True" 
            "RenderType"="Opaque" 
            "PreviewType"="Plane"
            "ForceNoShadowCasting"="True"
        }

        Cull Off
        Lighting Off
        ZTest Off
        ZWrite Off
        //Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            static const fixed3 MainOpaqueColor = fixed3(0.0, 0.0, 0.0);
            #include "LensFlaresCG.cginc"
        ENDCG
        }
    }

    SubShader
    {
        Tags
        { 
            "Queue" = "Geometry"
            "IgnoreProjector"="True" 
            "RenderType"="Occludie" 
            "PreviewType"="Plane"
            "ForceNoShadowCasting"="True"
        }

        Cull Off
        Lighting Off
        ZTest Off
        ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            static const fixed3 MainOpaqueColor = fixed3(1.0, 1.0, 0.0);
            #include "LensFlaresCG.cginc"
        ENDCG
        }
    }
}
