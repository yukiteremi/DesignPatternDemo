Shader "Custom/TransitionByTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TransitionTex ("Transition Texture", 2D) = "white" {}
        _Color("Screen Color", Color) = (1,1,1,1)
        _Cutoff ("Cutoff", range(0, 1)) = 0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            float4 _MainTex_TexelSize;
            sampler2D _MainTex;
            sampler2D _TransitionTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _Cutoff;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv1 = v.uv;
                
                // Flip sampling of the Texture: 
                // The main Texture
                // texel size will have negative Y).
                #if UNITY_UV_STARTS_AT_TOP
                if (_MainTex_TexelSize.y < 0)
                {
                    o.uv1.y = 1 - o.uv1.y;
                }
                #endif
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 transit = tex2D(_TransitionTex, i.uv);
                if (transit.b < _Cutoff)
                {
                    return _Color;
                }
                
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
