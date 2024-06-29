Shader "Custom/LineRendererRotatedUV"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Rotation ("UV Rotation", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _Rotation;

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
                float2x2 rotationMatrix = float2x2(cos(_Rotation * 6.28318), -sin(_Rotation * 6.28318),
                                                   sin(_Rotation * 6.28318), cos(_Rotation * 6.28318));
                o.uv = mul(rotationMatrix, v.uv - 0.5) + 0.5;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
