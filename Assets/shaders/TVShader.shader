Shader "Hidden/TVShader"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_VertsColor("Verts fill color", Float) = 0 
		_VertsColor2("Verts fill color 2", Float) = 0
		_VertsColorwidth("Width", Float) = 3
		_bwBlend ("Black & White blend", Range (0, 1)) = 0.5
    }
 
    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off Fog { Mode off }
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"
            #pragma target 3.0
 
            struct v2f
            {
                float4 pos      : POSITION;
                float2 uv       : TEXCOORD0;
                float4 scr_pos  : TEXCOORD1;
            };
 
            uniform sampler2D _MainTex;
            uniform float _VertsColorwidth;
			uniform float _VertsColor;
			uniform float _VertsColor2;
			uniform float _bwBlend;
 
            v2f vert(appdata_img v)
            {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
                
                o.scr_pos = ComputeScreenPos(o.pos);
                
                return o;
            }
 
            float4 frag(v2f i): COLOR
            {
                float4 color = tex2D(_MainTex, i.uv);
                
                //grey scale
				float4 c = tex2D(_MainTex, i.uv);
				
				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3( lum, lum, lum ); 
				
				float4 result = c;
				result.rgb = lerp(c.rgb, bw, _bwBlend);
                
                //make lines
                float2 ps = i.scr_pos.xy *_ScreenParams.xy / i.scr_pos.w;
				int pp = (int)ps.x % (3 * _VertsColorwidth);
				
				float4 muls =  float4(0, 0, 0, 1);			
				
				if (pp == 1) { muls.r = 1; muls.g = _VertsColor2; muls.b = _VertsColor; }
				    else if (pp == 2) { muls.g = 1; muls.b = _VertsColor2; muls.r = _VertsColor; }
				        else { muls.b = 1; muls.r = _VertsColor2;  muls.g = _VertsColor;  } 
				        
				return result * muls;
            }
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}
