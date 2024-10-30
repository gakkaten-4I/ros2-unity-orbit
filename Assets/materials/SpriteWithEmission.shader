Shader "Custom/SpritesWithEmission"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (0,0,0,1) // エミッションの色
        _EmissionStrength ("Emission Strength", Range(0, 100)) = 1.0 // エミッションの強さ
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        Blend One OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                half2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _EmissionColor;
            float _EmissionStrength;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texcol = tex2D(_MainTex, i.texcoord) * i.color;
                fixed4 emission = _EmissionColor * _EmissionStrength;

                // エミッションの合成
                texcol.rgb += emission.rgb;

                return texcol;
            }
            ENDCG
        }
    }
}
