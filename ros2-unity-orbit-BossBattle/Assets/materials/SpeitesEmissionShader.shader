Shader "Custom/SpritesEmission"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (0,0,0,0) // Emissionのカラー
        _EmissionPower ("Emission Power", Float) = 1.0 // Emissionの強さ
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
        Lighting Off
        ZWrite Off
        Cull Off
        Fog { Mode Off }
        Blend One OneMinusSrcAlpha

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
            float _EmissionPower;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;

                // Emissionの適用
                fixed4 emission = _EmissionColor * _EmissionPower;

                return col + emission;
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}
