Shader "Custom/CustomShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Albedo ("Albedo", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1) 
    }
    
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Albedo;
            float2 uv_NormalMap;
        };
        
        sampler2D _MainTex;
        sampler2D _Albedo;
        sampler2D _NormalMap;
        
        fixed4 _BaseColor;
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 mainTex = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 albedo = tex2D (_Albedo, IN.uv_Albedo);
            fixed3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
            
            o.Albedo = albedo.rgb * _BaseColor.rgb;
            o.Normal = normal;
            o.Alpha = 1.0;
        }
        
        ENDCG
    }
    
    FallBack "Diffuse"
}