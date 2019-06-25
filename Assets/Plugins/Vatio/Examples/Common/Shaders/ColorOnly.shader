// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Vatio/ColorOnly" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
	}

	SubShader {
		Pass {
			Cull Off

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
	
				fixed4 _Color;

				struct app2v {
					float4 vertex : POSITION;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					float4 color : COLOR0;
				};

				v2f vert (app2v v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.color = _Color;
					return o;
				}

				float4 frag (v2f i) : COLOR
				{
					return i.color;
				}
			ENDCG

		    }
		}
	FallBack "Diffuse"
}
