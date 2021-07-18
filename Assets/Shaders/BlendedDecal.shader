Shader "BlendedDecal"
{
	Properties
	{
		_Color("Tint", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
	}

		SubShader
	{
		Lighting Off
		ZTest LEqual
		ZWrite Off
		Tags{ "Queue" = "Transparent" }
		Pass
	{
		Alphatest Greater 0
		Blend SrcAlpha OneMinusSrcAlpha
		Offset -1, -1
		SetTexture[_MainTex]
	{
		ConstantColor[_Color]
		Combine texture * constant
	}
	}
	}
}