Shader "HD/Blender1X3" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base", 2D) = "white" {}
	_UITex("UIDecal", 2D) = "white" {}
	_UIColor("UIColor", Color) = (1,1,1,0)
	_SamTex ("Sample Tex", 2D) = "white" {}
	_AlphaTex ("Alpha Tex",2D) = "white"{}
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha

sampler2D _MainTex;
sampler2D _UITex;
sampler2D _SamTex;
sampler2D _AlphaTex;
fixed4 _Color;
fixed4 _UIColor;

struct Input {
	float2 uv_MainTex;
	float2 uv_SamTex;
	float2 uv_AlphaTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 cTexColor=tex2D(_SamTex,IN.uv_SamTex);
	float fxTexH=cTexColor.r*256.0f*255.0f;
	float fxTexL=cTexColor.g*255.0;
	float fxTex=(fxTexL+fxTexH)/65535.0f;
	
	float fyTexH=cTexColor.b * 256.0*255.0f;
	float fyTexL=cTexColor.a * 255.0;
	float fyTex=(fyTexL+fyTexH)/65535.0f;
	
	float2 tempTex={fxTex,1-fyTex};
	
	fixed4 c = tex2D(_MainTex, tempTex) * _Color;

	//’‚¿ÔÃÌº”UIÕºœÒ
	fixed4 d = tex2D(_UITex, tempTex) * _UIColor;
	c.rgb = lerp(c.rgb, d.rgb, d.a);
	c *= _Color;

	o.Emission = c.rgb;
	fixed4 alpha = tex2D(_AlphaTex,IN.uv_AlphaTex);
	o.Alpha = (alpha.r+alpha.g+alpha.b)/3.0f;
}
ENDCG
}

Fallback "Transparent/VertexLit"
}
