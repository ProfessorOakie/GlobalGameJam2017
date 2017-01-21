// GlowOnTouch scripts and shaders were written by Drew Okenfuss.
// Any object with this shader must have the GlowOnTouchSingle.cs script on it.

Shader "Custom/Sonar_Single" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
		float3 worldPos;
	};

	float3 _hitPt;
	float _StartTime;
	float _Intensity;

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		
		float d = distance(_hitPt, IN.worldPos);

		// Set color based on its radius and radius changes over time in the script
		if (d < _Time.y - _StartTime && d > _Time.y - _StartTime - 0.01 && (1 - (d / _Intensity)) > 0) {
			o.Albedo.x = c.rgb.x + (1 - (d / _Intensity));
			o.Albedo.y = c.rgb.y + (1 - (d / _Intensity));
			o.Albedo.z = c.rgb.z + (1 - (d / _Intensity));
		}
		else o.Albedo = c.rgb;

		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
