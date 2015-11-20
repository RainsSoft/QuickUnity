Shader "QuickUnity/Mobile/Toon Shader" {
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

			fixed4 _MainColor;
			fixed _OutlineWidth;
			fixed4 _OutlineColor;
			fixed _Factor;

			struct v2f {
				fixed4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};

			v2f vert(appdata_full v) {
				v2f o;
				fixed3 dir = normalize(v.vertex.xyz);
				fixed3 normal = v.normal;
				fixed d = dot(dir, normal);
				dir = dir * sign(d);
				dir = dir * _Factor + normal * (1 - _Factor);
				v.vertex.xyz += dir * _OutlineWidth;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = _OutlineColor;
				return o;
			}

			fixed4 frag(v2f i) : COLOR {
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

			fixed4 _MainColor;
			fixed4 _LightColor0;
			fixed _ToonLevel;
			fixed _ColorScaleLevels;
			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			fixed _RimPower;
			fixed _ToonRimColorScaleLevels;

			struct v2f {
				fixed4 pos : SV_POSITION;
				fixed3 lightDir : TEXCOORD0;
				fixed3 viewDir : TEXCOORD1;
				fixed3 normal : TEXCOORD2; 
				fixed2 uv_MainTex;
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

			fixed4 frag(v2f i) : COLOR {
				fixed4 c = 1;
				fixed4 mc = tex2D(_MainTex, i.uv_MainTex);
				fixed3 n = normalize(i.normal);
				fixed3 viewDir = normalize(i.viewDir);
				fixed3 lightDir = normalize(i.lightDir);
				fixed diff = max(0, dot(n, i.lightDir));
				diff = (diff + 1) / 2;
				diff = smoothstep(0, 1, diff);

				fixed toon = floor(diff * _ColorScaleLevels) / _ColorScaleLevels;
				diff = lerp(diff, toon, _ToonLevel);

				fixed rim = 1.0 - saturate(dot(n, normalize(viewDir)));
				rim = rim + 1;
				rim = pow(rim, _RimPower);
				fixed toonRim = floor(rim * _ToonRimColorScaleLevels);
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

			fixed4 _MainColor;
			fixed4 _LightColor0;
			fixed _ToonLevel;
			fixed _ColorScaleLevels;

			struct v2f {
				fixed4 pos : SV_POSITION;
				fixed3 lightDir : TEXCOORD0;
				fixed3 viewDir : TEXCOORD1;
				fixed3 normal : TEXCOORD2; 
			};

			v2f vert(appdata_full v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = v.normal;
				o.viewDir = ObjSpaceViewDir(v.vertex);
				o.lightDir = _WorldSpaceLightPos0 - v.vertex;
				return o;
			}

			fixed4 frag(v2f i) : COLOR {
				fixed4 c = 1;
				fixed3 n = normalize(i.normal);
				fixed3 viewDir = normalize(i.viewDir);
				fixed dist = length(i.lightDir);
				fixed3 lightDir = normalize(i.lightDir);
				fixed diff = max(0, dot(n, i.lightDir));
				diff = (diff + 1) / 2;
				diff = smoothstep(0, 1, diff);

				fixed atten = 1 / dist;
				fixed toon = floor(diff * atten * _ColorScaleLevels) / _ColorScaleLevels;
				diff = lerp(diff, toon, _ToonLevel);

				fixed3 h = normalize(lightDir + viewDir);
				fixed nh = max(0, dot(n, h));
				fixed spec = pow(nh, 32.0);
				fixed toonSpec = floor(spec * atten * 2);
				spec = lerp(spec, toonSpec, _ToonLevel);
				c = _MainColor * _LightColor0 * (diff + spec);
				return c;
			}
			ENDCG  
		}
	}

	Fallback "Diffuse"
}