using UnityEngine;

public class DayNightToggle : MonoBehaviour
{
    // Static variable to store the toggle value globally
    private static float globalToggleValue = 0.0f;

    void Update()
    {
        // Check for user input to toggle day/night
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDayNight();
        }
    }

    void ToggleDayNight()
    {
        // Toggle the global value between 0 and 1
        globalToggleValue = (globalToggleValue > 0.5f) ? 0.0f : 1.0f;

        // Find all objects using the DayNightShader and update the toggle value
        DayNightShader[] dayNightShaders = FindObjectsOfType<DayNightShader>();
        foreach (DayNightShader shader in dayNightShaders)
        {
            shader.SetToggleValue(globalToggleValue);
        }
    }

    // Getter method for other scripts to access the global toggle value
    public static float GetGlobalToggleValue()
    {
        return globalToggleValue;
    }
}
