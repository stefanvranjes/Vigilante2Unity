// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "PSXEffects/PS1Billboard3"
{
	Properties
	{
		[Toggle] _ColorOnly("Color Only", Float) = 0.0
		[Toggle] _Unlit("Unlit", Float) = 0.0
		[Toggle] _DrawDist("Affected by Polygonal Draw Distance", Float) = 1.0
		_VertexInaccuracy("Vertex Inaccuracy Override", Float) = -1.0
		_OffsetFactor("Offset factor", Float) = 1.0
		_OffsetUnits("Offset units", Float) = 1.0
		_Color("Color", Color) = (1,1,1,1)
		[KeywordEnum(Vertex, Fragment)] _DiffModel("Diffuse Model", Float) = 0.0
		_MainTex("Texture", 2D) = "white" {}
		_LODTex("LOD Texture", 2D) = "white" {}
		_LODAmt("LOD Amount", Float) = 0.0
		_NormalMap("Normal Map", 2D) = "bump" {}
		_NormalMapDepth("Normal Map Depth", Float) = 1
		[KeywordEnum(Gouraud, Phong)] _SpecModel("Specular Model", Float) = 0.0
		_SpecularMap("Specular Map", 2D) = "white" {}
		_Specular("Specular Amount", Float) = 0.0
		_MetalMap("Metal Map", 2D) = "white" {}
		_Metallic("Metallic Amount", Range(0.0,1.0)) = 0.0
		_Smoothness("Smoothness Amount", Range(0.0,1.0)) = 0.5
		_Emission("Emission Map", 2D) = "white" {}
		_EmissionAmt("Emission Amount", Float) = 0.0
		_Cube("Cubemap", Cube) = "" {}

		[HideInInspector] _SrcBlend("__src", Float) = 1.0
		[HideInInspector] _DstBlend("__dst", Float) = 0.0
		[HideInInspector] _ZWrite("__zw", Float) = 1.0
		[HideInInspector] _Cul("__cul", Float) = 0.0
		[HideInInspector] _BlendOp("__bld", Float) = 0.0

		[HideInInspector] _RenderMode("__rnd", Float) = 0.0
	}

	SubShader
	{
		Tags { "Queue"="Overlay" "DisableBatching" = "True" }
		LOD 100
		Blend One One // additive blending for a simple "glow" effect
		Cull Off // render backfaces as well
		ZWrite Off // don't write into the Z-buffer, this effect shouldn't block objects

		Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
 
            #pragma shader_feature IGNORE_ROTATION_AND_SCALE
            #pragma vertex vert
            #pragma fragment frag
 
            // User-specified uniforms          
            uniform sampler2D _MainTex;      
            uniform float _ScaleX;
            uniform float _ScaleY;
           
            float4 _MainTex_ST;
 
            struct vertexInput
            {
                float4 vertex : POSITION;
                float4 tex : TEXCOORD0;
            };
 
            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float2 tex : TEXCOORD0;
            };
            vertexOutput vert(vertexInput input)
            {
                // The world position of the center of the object
                float3 worldPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
 
                // Distance between the camera and the center
                float3 dist = _WorldSpaceCameraPos - worldPos;
 
                // atan2(dist.x, dist.z) = atan (dist.x / dist.z)
                // With atan the tree inverts when the camera has the same z position
                float angle = atan2(dist.x, dist.z);
 
                float3x3 rotMatrix;
                float cosinus = cos(angle);
                float sinus = sin(angle);
       
                // Rotation matrix in Y
                rotMatrix[0].xyz = float3(cosinus, 0, sinus);
                rotMatrix[1].xyz = float3(0, 1, 0);
                rotMatrix[2].xyz = float3(- sinus, 0, cosinus);
 
                // The position of the vertex after the rotation
                float4 newPos = float4(mul(rotMatrix, input.vertex * float4(1, 1, 0, 0)), 1);
 
                // The model matrix without the rotation and scale
                float4x4 matrix_M_noRot = unity_ObjectToWorld;
                matrix_M_noRot[0][0] = 1;
                matrix_M_noRot[0][1] = 0;
                matrix_M_noRot[0][2] = 0;
 
                matrix_M_noRot[1][0] = 0;
                matrix_M_noRot[1][1] = 1;
                matrix_M_noRot[1][2] = 0;
 
                matrix_M_noRot[2][0] = 0;
                matrix_M_noRot[2][1] = 0;
                matrix_M_noRot[2][2] = 1;
 
                vertexOutput output;
 
                // The position of the vertex in clip space ignoring the rotation and scale of the object
                #if IGNORE_ROTATION_AND_SCALE
                output.pos = mul(UNITY_MATRIX_VP, mul(matrix_M_noRot, newPos));
                #else
                output.pos = mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, newPos));
                #endif
 
                output.tex = TRANSFORM_TEX(input.tex, _MainTex);
 
                return output;
            }
            float4 frag(vertexOutput input) : COLOR
            {
                return tex2D(_MainTex, input.tex.xy);  
            }
            ENDCG
        }
	}
	CustomEditor "PS1ShaderEditor"

}
