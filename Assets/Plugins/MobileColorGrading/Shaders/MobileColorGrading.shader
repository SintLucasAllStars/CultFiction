Shader "Hidden/ALIyerEdon/ColorGrading" {
	Properties {
		_Color("Color",Vector) = (1,0,0,1)
		_Contrast("Contrast",Float) = 1
		_Exposure("Exposure",Float) = 1
		_Gamma("Gamma",Float) = 1
		_VignetteIntensity("_VignetteIntensity",Float) = .3
		_Saturation("Saturatiob",Float) = 1
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader 
	{
// No culling or depth
		Cull Off ZWrite Off ZTest Always
		   
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			#pragma multi_compile ACES_ON ACES_OFF
			#pragma multi_compile SaturN_ON SaturN_OFF
			#pragma multi_compile Vignette_ON Vignette_OFF

			uniform sampler2D _MainTex;

			half4 _Color;
			float _Contrast;
			float _Exposure;
			float _Gamma;
			float _VignetteIntensity;
			float _Saturation;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}


			half3 AdjustContrast(half3 color, half contrast) 
			{
				#if !UNITY_COLORSPACE_GAMMA
				    color = LinearToGammaSpace(color);
				#endif
				    color = saturate(lerp(half3(0.5, 0.5, 0.5), color, contrast));
				#if !UNITY_COLORSPACE_GAMMA
				    color = GammaToLinearSpace(color);
				#endif
				    return color;
			}


			half3 apply_lut(sampler2D tex, half3 uvw, half3 scaleOffset)
			{
			    // Strip format where `height = sqrt(width)`
			    uvw.z *= scaleOffset.z;
			    half shift = floor(uvw.z);
			    uvw.xy = uvw.xy * scaleOffset.z * scaleOffset.xy + scaleOffset.xy * 0.5;
			    uvw.x += shift * scaleOffset.y;
			    uvw.xyz = lerp(tex2D(tex, uvw.xy).rgb, tex2D(tex, uvw.xy + half2(scaleOffset.y, 0)).rgb, uvw.z - shift);
			    return uvw;
			}

			float3 ACESFilm( float3 x )
			{
			    float a = 2.51f;
			    float b = 0.03f;
			    float c = 2.43f;
			    float d = 0.59f;
			    float e = 0.14f;
			    return saturate((x*(a*x+b))/(x*(c*x+d)+e));
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float4 c = tex2D(_MainTex, i.uv);

                float4 fColor = float4(1,1,1,1) ;

                // Contrast
                fColor.rgb = AdjustContrast(c, _Contrast);

                // Brightness
              	//fColor.rgb *=  pow(2.0,_Exposure);

              	#ifdef SaturN_ON
              	// Saturation Calculation 
				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3( lum, lum, lum ); 

              	fColor.rgb = lerp(fColor.rgb, bw, _Saturation);
              	#endif              

                // Gamma
                fColor.rgb = pow(fColor, 1.0 / _Gamma) ;

                #ifdef Vignette_ON
                // Vignette
                half2 coords = i.uv;
				half2 uv = i.uv;
		
				coords = (coords - 0.5) * 2.0;		
				half coordDot = dot (coords,coords);
		 		 
				float mask = 1.0 - coordDot * _VignetteIntensity; 

				#endif

                // Color
                fColor.rgb  = float4(fColor.r + _Color.r,fColor.g + _Color.g,fColor.b + _Color.b,fColor.a + _Color.a);

                // ACES Tonemapping
              	#ifdef ACES_ON

            	float3 f = ACESFilm(fColor.rgb * 5 * _Exposure);

              	fColor = fixed4(f.r,f.g,f.b,fColor.a);

              	#endif

              	#ifdef Vignette_ON
				return fColor * mask;
				#else
				return fColor ;
				#endif
			}


						ENDCG
		}		
	}
}   