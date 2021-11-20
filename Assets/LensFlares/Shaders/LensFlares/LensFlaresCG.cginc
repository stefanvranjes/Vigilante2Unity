#ifndef LENS_FLARES_CG_CGINC
#define LENS_FLARES_CG_CGINC

#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex   : POSITION;
    float4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
};

struct v2f
{
    float4 vertex   : SV_POSITION;
    float4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
};

float4 _MainTex_ST;
sampler2D _MainTex;

v2f vert(appdata_t IN)
{
    v2f OUT;
    OUT.vertex = UnityObjectToClipPos(IN.vertex);
    OUT.color = IN.color;
    OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);

    return OUT;
}

fixed4 frag(v2f IN) : SV_Target
{
    fixed alpha = tex2D( _MainTex, IN.texcoord ).a * IN.color.a;
    fixed4 color = fixed4(MainOpaqueColor, alpha);
    return color;
}

#endif // LENS_FLARES_CG_CGINC