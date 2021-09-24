Shader "PSXEffects/PS1Screen"
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
		[HideInInspector] _Cul("__cul", Float) = 0.0
		[HideInInspector] _BlendOp("__bld", Float) = 0.0

		[HideInInspector] _RenderMode("__rnd", Float) = 0.0
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "DisableBatching" = "True" }
		LOD 100
		Cull Off // render backfaces as well
		ZWrite Off // don't write into the Z-buffer, this effect shouldn't block objects
		ZTest Always

		Pass
		{
			//Blend One One
			//SetTexture[_MainTex]{ combine texture }
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
                UNITY_FOG_COORDS(1)
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				//float scaleX = length(float4(UNITY_MATRIX_M[0].r, UNITY_MATRIX_M[1].r, UNITY_MATRIX_M[2].r, UNITY_MATRIX_M[3].r));
				//float scaleY = length(float4(UNITY_MATRIX_M[0].g, UNITY_MATRIX_M[1].g, UNITY_MATRIX_M[2].g, UNITY_MATRIX_M[3].g));

				/*o.vertex = mul(UNITY_MATRIX_P,
					mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
					+ float4(v.vertex.x, v.vertex.y, 0.0, 0.0)
					* float4(scaleX, scaleY, 1.0, 1.0));*/

                //o.vertex = UnityObjectToClipPos(o.vertex);
				o.vertex= UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				i.color.a = 1;
				fixed4 col = i.color;
				col.rgb = saturate(col.rgb);

#if !UNITY_COLORSPACE_GAMMA
				col.rgb = GammaToLinearSpace(col.rgb * 1.1);
#endif
                return col;
            }
			ENDCG
		}

		/*Pass
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
				float4 color : COLOR;
            };
 
            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float2 tex : TEXCOORD0;
				fixed4 color : COLOR;
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

				float scaleX = length(float4(UNITY_MATRIX_M[0].r, UNITY_MATRIX_M[1].r, UNITY_MATRIX_M[2].r, UNITY_MATRIX_M[3].r));
				float scaleY = length(float4(UNITY_MATRIX_M[0].g, UNITY_MATRIX_M[1].g, UNITY_MATRIX_M[2].g, UNITY_MATRIX_M[3].g));
 
                // The position of the vertex after the rotation
                float4 newPos = float4(mul(rotMatrix, input.vertex * float4(scaleX, scaleY, 1, 1)), 1);
 
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
				output.color = input.color;
 
                return output;
            }
            float4 frag(vertexOutput input) : SV_Target
            {
				input.color.a = 1;
				fixed4 col = input.color;
				col.rgb = saturate(col.rgb);

#if !UNITY_COLORSPACE_GAMMA
				col.rgb = GammaToLinearSpace(col.rgb * 1.1);
#endif
                return col;  
            }
            ENDCG
        }*/
	}

}
