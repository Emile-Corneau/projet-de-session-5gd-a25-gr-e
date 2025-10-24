Shader "UI/ThreeColorGradient" {
    Properties {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _Color ("Left Color", Color) = (1,1,1,1)
        _ColorMid ("Middle Color", Color) = (1,1,1,1)
        _Color2 ("Right Color", Color) = (1,1,1,1)
        _MidPoint ("Middle Point (0-1)", Range(0,1)) = 0.5
        _Scale ("Gradient Scale", Float) = 1

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader {
        Tags {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="False"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            Name "Default"

            Stencil {
                Ref [_Stencil]
                Comp [_StencilComp]
                Pass [_StencilOp]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
            }

            ColorMask [_ColorMask]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;
            fixed4 _ColorMid;
            fixed4 _Color2;
            float _MidPoint;
            float _Scale;

            struct appdata_t {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 position : SV_POSITION;
                float2 uv       : TEXCOORD0;
                fixed4 color    : COLOR;
            };

            v2f vert(appdata_t v) {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float2 uv = i.uv;

                // Compute the horizontal gradient
                float x = saturate(uv.x * _Scale);
                fixed4 col;

                if (_MidPoint <= 0.0) {
                    col = lerp(_ColorMid, _Color2, x);
                }
                else if (_MidPoint >= 1.0) {
                    col = lerp(_Color, _ColorMid, x);
                }
                else if (x < _MidPoint) {
                    float t = x / _MidPoint;
                    col = lerp(_Color, _ColorMid, t);
                }
                else {
                    float t = (x - _MidPoint) / (1.0 - _MidPoint);
                    col = lerp(_ColorMid, _Color2, t);
                }

                // Add edge fading: smooth transitions at top and bottom (and optional sides)
                float fadeX = smoothstep(0.0, 0.05, uv.x) * smoothstep(1.0, 0.95, uv.x);
                float fadeY = smoothstep(0.0, 0.05, uv.y) * smoothstep(1.0, 0.95, uv.y);

                // Multiply both horizontal and vertical fades together
                float edgeFade = fadeX * fadeY;

                // Combine the color and texture
                fixed4 texColor = tex2D(_MainTex, uv);
                fixed4 finalColor = texColor * col * edgeFade;

                return finalColor;
            }



            ENDCG
        }
    }

    FallBack "UI/Default"
}
