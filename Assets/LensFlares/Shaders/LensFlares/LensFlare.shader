Shader "+dotsquid/LensFlare/Flare"
{
    Properties
    {
        [PerRendererData]   _MainTex        ("Sprite Texture", 2D) = "white" {}
                            _OcclusionMap   ("Occlusion Map", any) = "white" {}
                            _MinLevel       ("Min Level", Float) = 0.0
                            _MaxLevel       ("Max Level", Float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Overlay"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One One
        ColorMask RGB

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            static const float _AbsLevel = 0.7853; // ratio of circle area to square area

            sampler2D _MainTex;
            sampler2D _OcclusionMap;
            float4 _MainTex_ST;
            float4 _OcclusionMap_TexelSize;
            float _MinLevel;
            float _MaxLevel;

            struct appdata_t {
                float4 vertex    : POSITION;
                fixed4 color     : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 offset    : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex    : SV_POSITION;
                fixed4 color     : COLOR;
                float2 texcoord  : TEXCOORD0;
                float  intensity : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float GetIntensity()
            {
                float mipLevel = log2(max(_OcclusionMap_TexelSize.z, _OcclusionMap_TexelSize.w));
                float4 occlusionUV = float4(0.5, 0.5, 0.0, mipLevel);
                fixed occlusion = tex2Dlod(_OcclusionMap, occlusionUV);
                float intensity = smoothstep(_MinLevel, _MaxLevel, occlusion / _AbsLevel);
                return intensity;
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                float intensity = GetIntensity();

                bool canScale = v.offset.z > 0.0;
                float randomSeed = v.offset.w;
                float3 dir = float3(v.offset.xy, 0.0);
                float3 center = v.vertex.xyz - dir;
                float scale = canScale ? sqrt(intensity) : 1.0;
                float3 offset = dir * scale;
                float3 vertex = center + offset;

                o.vertex = UnityObjectToClipPos(vertex);
                o.color = v.color;
                o.texcoord = v.texcoord;
                o.intensity = intensity;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.texcoord) * i.color;
                float alpha = i.intensity * i.intensity;
                color.rgb *= alpha * color.a;
                return color * 1.5;
            }
            ENDCG
        }
    }
}
