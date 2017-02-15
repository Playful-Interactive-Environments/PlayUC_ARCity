Shader "Custom/MaskedShader" {
	Properties {
		_ColorR1("ColorR Mask1", Color) = (1,1,1,1)
		_ColorG1("ColorG Mask1", Color) = (1,1,1,1)
		_ColorB1("ColorB Mask1", Color) = (1,1,1,1)
		_ColorR2("ColorR Mask2", Color) = (1,1,1,1)
		_ColorG2("ColorG Mask2", Color) = (1,1,1,1)
		_ColorB2("ColorB Mask2", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "black" {}
		_Mask1Tex("Mask1", 2D) = "black" {}
		_Mask2Tex("Mask2", 2D) = "black" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Saturation ("Saturation", Range(0,1)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex, _Mask1Tex, _Mask2Tex;

		struct Input {
			float2 uv_MainTex, uv_Mask1Tex, uv_Mask2Tex;
		};

		half _Glossiness;
		half _Metallic;
		half _Saturation;
		fixed4 _ColorR1, _ColorG1, _ColorB1, _ColorR2, _ColorG2, _ColorB2;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 mask1 = tex2D(_Mask1Tex, IN.uv_Mask1Tex);
			fixed4 mask2 = tex2D(_Mask2Tex, IN.uv_Mask2Tex);
			//c.rgb = c.rgb * (1 - mask.r) + _ColorR * mask.R;
			c.rgb = c.rgb + _ColorR1 * mask1.r + _ColorG1 * mask1.g + _ColorB1 * mask1.b;
			c.rgb = c.rgb + _ColorR2 * mask2.r + _ColorG2 * mask2.g + _ColorB2 * mask2.b;

			float grayScale = (float)((c.rgb.r * 0.3) + (c.rgb.g * 0.59) + (c.rgb.b * 0.11));
			fixed4 g = (grayScale, grayScale, grayScale, grayScale);
			c.rgb = c.rgb * _Saturation + g.rgb * (1 - _Saturation);



			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

//https://www.youtube.com/watch?v=KwQb1_yYMA0
