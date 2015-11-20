Shader "QuickUnity/Toon Shader" {
	Properties {
		_MainColor ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Main Texure", 2D) = "white" {}
		_OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.02
		_OutlineColor ("Outline Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Factor ("Factor", Range(0.0, 1.0)) = 0.5
		_ToonLevel ("Toon Level", Range(0.0, 1.0)) = 0.5
		_ColorScaleLevels ("Color Scale Levels", Range(0.0, 9.0)) = 3.0
		_RimPower ("Rim Power", Range(0.1, 10.0)) = 5.0
		_ToonRimColorScaleLevels ("Toon Rim Color Scale Levels", Range(0.0, 9.0)) = 3.0
	}
	SubShader {
		Pass {
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"  

			float4 _MainColor;
			float _OutlineWidth;
			float4 _OutlineColor;
			float _Factor;

			struct v2f {
				float4 pos : SV_POSITION;
				float4 color : COLOR;
			};

			v2f vert(appdata_full v) {
				v2f o;
				float3 dir = normalize(v.vertex.xyz);
				float3 normal = v.normal;
				float d = dot(dir, normal);
				dir = dir * sign(d);
				dir = dir * _Factor + normal * (1 - _Factor);
				v.vertex.xyz += dir * _OutlineWidth;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = _OutlineColor;
				return o;
			}

			float4 frag(v2f i) : COLOR {
				return i.color;
			}

			ENDCG
		}

		Pass {
			Tags { "LightMode" = "ForwardBase" }
			Cull Back
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members uv_MainTex)
#pragma exclude_renderers d3d11 xbox360
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float4 _MainColor;
			float4 _LightColor0;
			float _ToonLevel;
			float _ColorScaleLevels;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _RimPower;
			float _ToonRimColorScaleLevels;

			struct v2f {
				float4 pos : SV_POSITION;
				float3 lightDir : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				float3 normal : TEXCOORD2; 
				float2 uv_MainTex;
			};

			v2f vert(appdata_full v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = v.normal;
				o.lightDir = ObjSpaceLightDir(v.vertex);
				o.viewDir = ObjSpaceViewDir(v.vertex);
				o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			float4 frag(v2f i) : COLOR {
				float4 c = 1;
				half4 mc = tex2D(_MainTex, i.uv_MainTex);
				float3 n = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);
				float3 lightDir = normalize(i.lightDir);
				float diff = max(0, dot(n, i.lightDir));
				diff = (diff + 1) / 2;
				diff = smoothstep(0, 1, diff);

				float toon = floor(diff * _ColorScaleLevels) / _ColorScaleLevels;
				diff = lerp(diff, toon, _ToonLevel);

				float rim = 1.0 - saturate(dot(n, normalize(viewDir)));
				rim = rim + 1;
				rim = pow(rim, _RimPower);
				float toonRim = floor(rim * _ToonRimColorScaleLevels);
				rim = lerp(rim, toonRim, _ToonLevel);

				c = _MainColor * _LightColor0 * diff * rim * mc * 2;
				return c;
			}

			ENDCG
		}

		Pass {
			Tags { "LightMode" = "ForwardAdd" }
			Blend One One
			Cull Back
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"  

			float4 _MainColor;
			float4 _LightColor0;
			float _ToonLevel;
			float _ColorScaleLevels;

			struct v2f {
				float4 pos : SV_POSITION;
				float3 lightDir : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				float3 normal : TEXCOORD2; 
			};

			v2f vert(appdata_full v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = v.normal;
				o.viewDir = ObjSpaceViewDir(v.vertex);
				o.lightDir = _WorldSpaceLightPos0 - v.vertex;
				return o;
			}

			float4 frag(v2f i) : COLOR {
				float4 c = 1;
				float3 n = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);
				float dist = length(i.lightDir);
				float3 lightDir = normalize(i.lightDir);
				float diff = max(0, dot(n, i.lightDir));
				diff = (diff + 1) / 2;
				diff = smoothstep(0, 1, diff);

				float atten = 1 / dist;
				float toon = floor(diff * atten * _ColorScaleLevels) / _ColorScaleLevels;
				diff = lerp(diff, toon, _ToonLevel);

				half3 h = normalize(lightDir + viewDir);
				float nh = max(0, dot(n, h));
				float spec = pow(nh, 32.0);
				float toonSpec = floor(spec * atten * 2);
				spec = lerp(spec, toonSpec, _ToonLevel);
				c = _MainColor * _LightColor0 * (diff + spec);
				return c;
			}
			ENDCG  
		}
	}

	Fallback "Diffuse"
}