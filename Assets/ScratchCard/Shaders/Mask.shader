Shader "ScratchCard/Mask"
{
    Properties
    {
        _MainTex ("Main", 2D) = "white" {}
        _MaskTex ("Mask", 2D) = "black" {}
        _Offset ("Offset", Vector) = (0, 0, 1, 1)

        [HideInInspector]_StencilComp ("Stencil Comparison", Float) = 8
        [HideInInspector]_Stencil("Stencil ID", Float) = 0
        [HideInInspector]_StencilOp ("Stencil Operation", Float) = 0
        [HideInInspector]_StencilWriteMask ("Stencil WriteMask", Float) = 255
        [HideInInspector]_StencilReadMask ("Stencil ReadMask", Float) = 255
        [HideInInspector]_ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "DisableBatching"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        ColorMask [_ColorMask]
        Pass
        {
            CGPROGRAM
            #pragma target 2.0
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile __ UNITY_UI_CLIP_RECT UNITY_UI_ALPHACLIP
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;
            float4 _MainTex_ST;
            float4 _MaskTex_ST;
            float4 _Offset;
            float4 _ClipRect;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            struct app2vert
            {
                float4 vertex : POSITION;
                half4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct vert2frag
            {
                float4 position : SV_POSITION;
                half4 color : COLOR;
                float2 uv : TEXCOORD0;
            #ifdef UNITY_UI_CLIP_RECT
                float2 worldPos : TEXCOORD1;
            #endif
            };

            vert2frag vert (app2vert v)
            {
                vert2frag o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

            #ifdef UNITY_UI_CLIP_RECT
                o.worldPos = (mul(unity_ObjectToWorld, v.vertex)).xy;
            #endif
                return o;
            }

            fixed4 frag (vert2frag i) : SV_Target
            {
                fixed4 mainCol = tex2D(_MainTex, float2(_Offset.x + i.uv.x * _Offset.z, _Offset.y + i.uv.y * _Offset.w));
                fixed maskVal = tex2D(_MaskTex, i.uv).r;
                fixed4 col;
                col.rgb = i.color.rgb * mainCol.rgb;
                col.a = i.color.a * mainCol.a * (1.0 - maskVal);

            #ifdef UNITY_UI_CLIP_RECT
                col.a *= UnityGet2DClipping(i.worldPos, _ClipRect, _UIMaskSoftnessX, _UIMaskSoftnessY);
            #endif

            #ifdef UNITY_UI_ALPHACLIP
                clip(col.a - 0.001);
            #endif
                return col;
            }
            ENDCG
        }
    }

    FallBack "UI/Default"
}
