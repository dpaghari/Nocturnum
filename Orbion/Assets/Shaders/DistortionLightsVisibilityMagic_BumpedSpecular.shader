Shader "Custom/SimpleDistortion" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_Distortion ("Distortion Level", Range(0, 200)) = 0.05
	_WTHScaling ("Darkness Shift", Range(0, 1)) = 0
}
SubShader { 
	Tags { "RenderType"="Opaque" "Queue" = "Transparent-1"}
	LOD 400
	
GrabPass{"_BackgroundGrab"}	

CGPROGRAM
#pragma surface surf _Distort_Reveal

struct SurfaceOutputCustom {
    fixed3 Albedo;
    fixed3 Normal;
    fixed3 Emission;
    half Specular;
    fixed Gloss;
    fixed Alpha;
    float2 ScreenSpaceUVFetch;
};

sampler2D _BackgroundGrab;
float2 _BackgroundGrab_TexelSize;
half _Distortion;
float _WTHScaling;


inline fixed4 Lighting_Distort_Reveal (SurfaceOutputCustom s, fixed3 lightDir, half3 viewDir, fixed atten)
{
	fixed4 distortedScene = tex2D(_BackgroundGrab, s.ScreenSpaceUVFetch + s.Normal * _BackgroundGrab_TexelSize * _Distortion) * _WTHScaling;

	half3 h = normalize (lightDir + viewDir);
	
	fixed diff = max (0, dot (s.Normal, lightDir));
	
	float nh = max (0, dot (s.Normal, h));
	float spec = pow (nh, s.Specular*128.0) * s.Gloss;
	
	fixed4 c;
	
	c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec) * (atten * 2);
	c.a = s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten;
	
	float3 DiffuseIlluminationFactors = _LightColor0.rgb * diff * (atten * 2);
	float maxIllumFactor = max(max(DiffuseIlluminationFactors.r, DiffuseIlluminationFactors.g), DiffuseIlluminationFactors.b);
	maxIllumFactor = saturate(maxIllumFactor * 5);
	
	return lerp(distortedScene, c, float4(maxIllumFactor,maxIllumFactor,maxIllumFactor,1));//lerp(distortedScene, c, saturate(maxIllumFactor) );
	
	//lerp(distortedScene, float4(s.Albedo, 1), float4(DiffuseIlluminationFactors, 1));
	
	// float4(s.Albedo * DiffuseIlluminationFactors, 1); //lerp(float4(0), float4(s.Albedo, 1), float4(DiffuseIlluminationFactors, 1));//lerp(distortedScene, c, maxIllumFactor);//lerp(distortedScene, c, saturate(maxIllumFactor) );
}

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;
half _Shininess;

struct Input {	
	float4 screenPos;
	float2 uv_MainTex;
	float2 uv_BumpMap;
};

void surf (Input IN, inout SurfaceOutputCustom o) {
	
	o.ScreenSpaceUVFetch = IN.screenPos.xy / IN.screenPos.w;

	#if UNITY_UV_STARTS_AT_TOP
	if (_ProjectionParams.x > 0)
		o.ScreenSpaceUVFetch.y = 1 - o.ScreenSpaceUVFetch.y;
	#endif

	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = tex.rgb * _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = tex.a * _Color.a;
	o.Specular = _Shininess;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
ENDCG
}

FallBack "Specular"
}
