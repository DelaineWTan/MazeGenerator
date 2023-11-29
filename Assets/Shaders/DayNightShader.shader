Shader"Custom/DayNightShader"
{
    Properties
    {
        _ToggleDayNight ("Toggle Day/Night", Range(0, 1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        fixed _ToggleDayNight;

        struct Input
        {
            float2 uv_ToggleDayNight;
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
        }
        ENDCG
    }
    FallBack "Diffuse"
}
