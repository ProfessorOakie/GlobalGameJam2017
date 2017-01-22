// GlowOnTouch scripts and shaders were written by Drew Okenfuss.
// Any object with this shader must have the GlowOnTouchSingle.cs script on it.

Shader "Custom/Sonar_Multi" {
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

	float4 _hitPts[20];
	float _StartTime;
	float _Intensity[20];
	float3 _TempColor;

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;

	float d;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

		o.Albedo = c.rgb;
		

		// Check every point in the array
		// goal is to set RGB to highest possible values based on tracked points
		for (int i = 0; i < 20; i++) {

			d = distance(_hitPts[i], IN.worldPos);

			//o.Albedo = _hitPts[19].w;
			if (d < _Time.y - _hitPts[i].w && d > _Time.y - _hitPts[i].w - 0.01 && (1 - (d / _Intensity[i])) > 0) {
				//o.Albedo = 0;
				float r = c.rgb.x + (1 - (d / _Intensity[i]))*8;
				float g = c.rgb.y + (1 - (d / _Intensity[i]))*8;
				float b = c.rgb.z + (1 - (d / _Intensity[i]))*8;

				// set RGB values if they are larger
				if (r > o.Albedo.x) o.Albedo.x = r;
				if (g > o.Albedo.y) o.Albedo.y = g;
				if (b > o.Albedo.z) o.Albedo.z = b;

				//}
			}
		}

			// Set color based on its radius and radius changes over time in the script
			/*if (d < _Time.y - _StartTime && d > _Time.y - _StartTime - 0.01 && (1 - (d / _Intensity)) > 0) {
				o.Albedo.x = c.rgb.x + (1 - (d / _Intensity));
				o.Albedo.y = c.rgb.y + (1 - (d / _Intensity));
				o.Albedo.z = c.rgb.z + (1 - (d / _Intensity));
			}
			else o.Albedo = c.rgb;
	*/
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
