Shader"Custom/DayNightFogShader"
{
    Properties
    {
        _ToggleDayNight ("Toggle Day/Night", Range(0, 1)) = 0.0
        _ToggleFog ("Toggle Fog", Range(0, 1)) = 0.0
        _FogColor ("Fog Color", Color) = (0.5, 0.5, 0.5, 1.0)
        _FogStart ("Fog Start", Range(0, 100)) = 0.0
        _FogEnd ("Fog End", Range(0, 100)) = 5.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        fixed _ToggleDayNight;
        fixed _ToggleFog;
        float4 _FogColor;
        float _FogStart;
        float _FogEnd;

        struct Input
        {
            float2 uv_ToggleDayNight;
            float4 pos : POSITION;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Adjust ambient lighting based on the toggle value
            if (_ToggleDayNight > 0.5)
            {
                o.Albedo = float3(1, 1, 1); // Day ambient color
                o.Specular = 0.5; // Day specular intensity
                o.Emission = float3(0, 0, 0); // No emission during the day
            }
            else
            {
                o.Albedo = float3(0.1, 0.1, 0.1); // Night ambient color
                o.Specular = 0.1; // Night specular intensity
                o.Emission = float3(0.2, 0.2, 0.2); // Night emission for lights
            }

            // Apply fog based on the toggle value
            if (_ToggleFog > 0.5)
            {
                float fogFactor = saturate((IN.pos.z - _FogStart) / (_FogEnd - _FogStart));
                o.Albedo = lerp(o.Albedo, _FogColor.rgb, fogFactor);
            }
        }
        ENDCG
    }
FallBack "Diffuse"
}
