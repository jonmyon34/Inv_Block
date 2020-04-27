Shader "Unlit/skybox"
{
	SubShader
	{
		Tags
		{
			"RenderType" = "Background"
			"Queue" = "Background"
			"PreviewType" = "SkyBox"
		}

		Pass
		{
			ZWrite Off
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float3 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 texcoord : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				//x,yが逆！
				float div = 8;
				if ((i.texcoord.y + 1 +_Time.w/40) * 100 % div<0.1&&i.texcoord.y>-100)
				return half4(0.01,0.65-i.texcoord.y*0.65, 0.65 - i.texcoord.y*0.65, 1);
				else
				return half4(0, 0, 0, 1);

				/*float x = 0.1;
				float2 p = float2(x, i.texcoord.y);
				return fixed4(0.1/length(p), 0, 0, 1.0);*/
				//return fixed4(lerp(fixed3(1, 0, 0), fixed3(0, 0, 1), i.texcoord.y * 0.5 + 0.5), 1.0);
			}
			ENDCG
		}
	}
}
