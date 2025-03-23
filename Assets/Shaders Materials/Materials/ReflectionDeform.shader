Shader "Custom/SineWaveShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Frequency ("Wave Frequency", Float) = 1.0
        _Amplitude ("Wave Amplitude", Float) = 0.1
        _Speed ("Wave Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        fixed4 _Color;
        float _Frequency;
        float _Amplitude;
        float _Speed;

        // Vertex modification function
        void vert (inout appdata_full v)
        {
            float wave = sin(_Frequency * v.vertex.y + _Speed * _Time.y);
            v.vertex.y += wave * _Amplitude;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = 1.0; // Always metallic
            o.Smoothness = 1.0; // Fully smooth
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
