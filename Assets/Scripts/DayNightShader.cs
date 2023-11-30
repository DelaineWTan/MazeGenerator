using UnityEngine;

public class DayNightShader : MonoBehaviour
{
    // Reference to the material
    [SerializeField] Material material;

    void Start()
    {
        // Get the material of the object
        material = GetComponent<Renderer>().material;

        // Set the initial toggle value based on the global value
        SetToggleValue(ToggleEffects.GetGlobalToggleValue());
    }

    // Method to set the toggle value in the shader
    public void SetToggleValue(float value)
    {
        material.SetFloat("_ToggleDayNight", value);
    }
}
