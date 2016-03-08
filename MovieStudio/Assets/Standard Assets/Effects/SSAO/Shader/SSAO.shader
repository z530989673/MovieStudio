Shader "SSAO/SSAO"
{
	Properties
	{
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass // ssao first pass
		{
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
			{
				float4 posH	   : SV_POSITION;
				float4 projPos : TEXCOORD0;
				float4 posV	   : TEXCOORD1;
				float2 uv	   : TEXCOORD2;
#if UNITY_UV_STARTS_AT_TOP
				float2 uv2     : TEXCOORD3;
#endif
			};

			uniform sampler2D _NoiseTexture;
			uniform sampler2D _MainTex;

			uniform float4 _OcclusionColor;
			uniform float4 _MainTex_TexelSize;

			uniform float4 _ParamBiasDis; // Bias / Sample Radius / Distance Cutoff / Cutoff Falloff
			uniform float4 _ParamSSAOAdjust; // Noise Size / Luminosity Contribution / Intensity / Distance

			sampler2D_float _CameraDepthTexture;
			sampler2D_float _CameraDepthNormalsTexture;

			float4x4 _ViewToWorldMatrix;
			float4x4 _CameraModelView;

			float3 GetVSPosition(float2 screenUV, float depth)
			{
				return float3(2 * screenUV - float2(1, 1), depth);
			}

			v2f vert(appdata_base i)
			{
				v2f o;

				o.posH = mul(UNITY_MATRIX_MVP, i.vertex);
				o.posV = mul(UNITY_MATRIX_MV, i.vertex);
				o.projPos = ComputeScreenPos(o.posH);
				o.uv = i.texcoord;

#if UNITY_UV_STARTS_AT_TOP
				o.uv2 = i.texcoord;
				if (_MainTex_TexelSize.y < 0.0)
					o.uv2.y = 1.0 - o.uv2.y;
#endif

				return o;
			}

			float GetDepth(float2 uv)
			{
				float depth = tex2D(_CameraDepthTexture, uv).x;
				return depth;
			}

			float3 GetWSPos(float2 uv, float depth)
			{
				float4 VSPos = float4(2 * uv - 1.0, depth, 1);
				float4 WSPos = mul(_ViewToWorldMatrix, VSPos);
				return WSPos.xyz / WSPos.w;
			}

			float3 GetWSNormal(float2 uv)
			{
				float3 nn = tex2D(_CameraDepthNormalsTexture, uv).xyz * float3(3.5554, 3.5554, 0) + float3(-1.7777, -1.7777, 1.0);
				float g = 2.0 / dot(nn.xyz, nn.xyz);
				float3 normal = float3(g * nn.xy, g - 1.0);

				float3 WSNormal = mul((float3x3)_CameraModelView, normal);
				return WSNormal;
			}

			float calcAO(float2 uv, float2 coord, float3 cPosition, float3 cNormal)
			{
				uv = coord + uv;

				float depth = GetDepth(uv);
				float3 position = GetWSPos(uv, depth);

				float3 diff = position - cPosition;
				float3 normalDiff = normalize(diff);
				float lenth = length(diff) *_ParamSSAOAdjust.w;

				return max(0.0, dot(cNormal, normalDiff) - _ParamBiasDis.x) * (1.0 / (1.0 + lenth)) *_ParamSSAOAdjust.z;
			}

			inline float invlerp(float from, float to, float value)
			{
				return (value - from) / (to - from);
			}

			float4 GetAOColor(float ao, float2 uv)
			{
				float3 color = tex2D(_MainTex, uv).rgb;
				float luminance = dot(color, float3(0.299, 0.587, 0.114));
				float aoFinal = lerp(ao, 1.0, luminance * _ParamSSAOAdjust.y);
				return float4(aoFinal, aoFinal, aoFinal, 1.0);
			}

			float SSAO(float2 uv)
			{
				const float2 cross[4] = { float2(1.0, 0.0), float2(-1.0, 0.0), float2(0.0, 1.0), float2(0.0, -1.0) };

				float viewDepth = GetDepth(uv);
				float depth = LinearEyeDepth(viewDepth);
				float3 position = GetWSPos(uv, viewDepth);
				float3 normal = GetWSNormal(uv);

				float2 random = normalize(2 * tex2D(_NoiseTexture, uv * (_ScreenParams.xy / _ParamSSAOAdjust.x)).rg - 1.0);
				float radius = _ParamBiasDis.y / depth; 
				//clip(_ParamBiasDis.z - depth);

				float ao = 0;
				for (int i = 0; i < 4; i++)
				{
					float2 coord1 = reflect(cross[i], random) * radius;
					float2 coord2 = coord1 * 0.707;
					coord2.x = coord2.x - coord2.y;
					coord2.y = coord2.x + coord2.y;

					ao += calcAO(uv, coord1 * 0.20, position, normal);
					ao += calcAO(uv, coord2 * 0.40, position, normal);
					ao += calcAO(uv, coord1 * 0.60, position, normal);
					ao += calcAO(uv, coord2 * 0.80, position, normal);
					ao += calcAO(uv, coord1, position, normal);
				}

				ao /= 20;
				ao = lerp(1.0 - ao, 1.0, saturate(invlerp(_ParamBiasDis.z - _ParamBiasDis.w, _ParamBiasDis.z, depth)));

				return ao;
			}

			float4 frag(v2f i) : COLOR
			{
#if UNITY_UV_STARTS_AT_TOP
				return saturate(GetAOColor(SSAO(i.uv), i.uv2) + _OcclusionColor);
#else
				return saturate(GetAOColor(SSAO(i.uv), i.uv) + _OcclusionColor);
#endif
			}
			ENDCG
		}

		Pass // Blur Pass
		{
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform float2 _Direction;
			uniform sampler2D _MainTex;

			struct v2f
			{
				float4 posH	 : SV_POSITION;
				float2 uv	 : TEXCOORD0;
				float4 uv1	 : TEXCOORD1;
				float4 uv2   : TEXCOORD2;
			};

			v2f vert(appdata_base i)
			{
				v2f o;
				o.posH = mul(UNITY_MATRIX_MVP, i.vertex);
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, i.texcoord);
				o.uv1 = float4(o.uv + 1.3846153846 * _Direction, o.uv - 1.3846153846 * _Direction);
				o.uv2 = float4(o.uv + 3.2307692308 * _Direction, o.uv - 3.2307692308 * _Direction);
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float3 c = tex2D(_MainTex, i.uv).rgb * 0.2270270270;
				c += tex2D(_MainTex, i.uv1.xy).rgb * 0.3162162162;
				c += tex2D(_MainTex, i.uv1.zw).rgb * 0.3162162162;
				c += tex2D(_MainTex, i.uv2.xy).rgb * 0.0702702703;
				c += tex2D(_MainTex, i.uv2.zw).rgb * 0.0702702703;
				return float4(c, 1);
			}
			ENDCG
		}

		Pass // Apply AO color
		{ 
			ZWrite Off
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _SSAOTex;

			struct v2f
			{
				float4 posH	 : SV_POSITION;
				float2 uv	 : TEXCOORD0;
#if UNITY_UV_STARTS_AT_TOP
				float2 uv2   : TEXCOORD2;
#endif
			};

			v2f vert(appdata_img v)
			{
				v2f o;
				o.posH = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;
#if UNITY_UV_STARTS_AT_TOP
				o.uv2 = float2(v.texcoord.x, 1 - v.texcoord.y);
#endif
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
#if UNITY_UV_STARTS_AT_TOP
				float4 color = tex2D(_MainTex, i.uv2).rgba;
#else
				float4 color = tex2D(_MainTex, i.uv).rgba;
#endif
				return float4(color.rgb * tex2D(_SSAOTex, i.uv).rgb, color.a);
			}

			ENDCG
		}
	}
}
