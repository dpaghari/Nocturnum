Shader "Custom/AntiLight" {
	Properties {
		_Color ("Color (RGB)", Color) = (1,1,1,1)
	}
	SubShader {
		Pass{
		
			ZWrite off
			Blend DstColor Zero //multiplicative blending
			
			Tags { "RenderType"="Opaque" "Queue" = "Transparent+1"}
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 

			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float4 absolutePosition : TEXCOORD0;
			};

			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.absolutePosition = mul (_Object2World, v.vertex);
				return o;
			}
			
			float4 _Color;
			float4 _LightCenterPos;

			float4 frag (v2f i) : COLOR
			{
				float dist = distance(_LightCenterPos.xyz, i.absolutePosition.xyz);
				dist = dist / _LightCenterPos.w; //have (0..1) range be such that 0 is 0 and w is 1; w equivalent to 'range' of a point light
				
				float strength = 1.0 / (1.0 + 25.0*dist*dist);
				
				float4 cMultiplier = lerp(float4(1,1,1,1), _Color, strength);
				
				return cMultiplier;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
