using UnityEngine;

public class DayNightFogShader : MonoBehaviour
{
    // Reference to the material
    private Material material;

    void Start()
    {
        // Get the material of the object
        material = GetComponent<Renderer>().material;

        // Set the initial toggle value based on the global value
        SetToggleValue(DayNightToggle.GetGlobalToggleValue());
    }

    // Method to set the toggle value in the shader
    public void SetToggleValue(float value)
    {
        material.SetFloat("_ToggleDayNight", value);
    }

    // Set fog toggle value from the DayNightToggle script
    public void SetFogToggleValue(float value)
    {
        material.SetFloat("_ToggleFog", value);
    }
}
