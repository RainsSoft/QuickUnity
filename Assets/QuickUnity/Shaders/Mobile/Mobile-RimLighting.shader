Shader "QuickUnity/Mobile/RimLighting" {
	Properties {
		_MainTex ("Main Texure", 2D) = "white" {}
		_RimColor ("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimPower ("Rim Power", Range(0.1, 10.0)) = 5.0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert

		struct Input {
			fixed2 uv_MainTex;
			fixed3 viewDir;
		};

		sampler2D _MainTex;
		fixed4 _RimColor;
		fixed _RimPower;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			fixed rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		}
		ENDCG
	}

	Fallback "Mobile/Diffuse"
}