using System;
using UnityEngine;

public class DayNightToggle : MonoBehaviour
{
    [SerializeField] GameObject DayMusic;
    [SerializeField] GameObject NightMusic;

    // Static variable to store the toggle value globally
    private static float globalDayNightToggleValue = 0.0f;
    private static float globalFogToggleValue = 1.0f;
    // Reference to current BGM
    private GameObject BGMAudioObject;

    void Start()
    {
        // Start as Day and play BGM
        ToggleDayNight();
        // Start with no Fog
        ToggleFog();
    }

    void Update()
    {
        // Check for user input to toggle day/night
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDayNight();
        }
        // Check for user input to toggle fog
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFog();
        }
        // Check for user input to toggle BGM on/off
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleBGM();
        }
    }

    void ToggleDayNight()
    {
        // Toggle the global value between 0 and 1
        globalDayNightToggleValue = (globalDayNightToggleValue > 0.5f) ? 0.0f : 1.0f;

        // Find all objects using the DayNightShader and update the toggle value
        DayNightFogShader[] dayNightShaders = FindObjectsOfType<DayNightFogShader>();
        foreach (DayNightFogShader shader in dayNightShaders)
        {
            shader.SetToggleValue(globalDayNightToggleValue);
        }

        if (BGMAudioObject != null)
            PlaySfx.StopLoopedAudio(BGMAudioObject);

        if (globalDayNightToggleValue == 1.0f)
            BGMAudioObject = PlaySfx.PlayWithLoop(DayMusic, transform);
        else if (globalDayNightToggleValue == 0.0f)
            BGMAudioObject = PlaySfx.PlayWithLoop(NightMusic, transform);
    }

    void ToggleFog()
    {
        // Toggle the global fog value between 0 and 1
        globalFogToggleValue = (globalFogToggleValue > 0.5f) ? 0.0f : 1.0f;
        Debug.Log(globalFogToggleValue);
        // Find all objects using the DayNightFogShader and update the fog toggle value
        DayNightFogShader[] dayNightFogShaders = FindObjectsOfType<DayNightFogShader>();
        foreach (DayNightFogShader fogShader in dayNightFogShaders)
        {
            fogShader.SetFogToggleValue(globalFogToggleValue);
        }
    }

    void ToggleBGM()
    {
        if (BGMAudioObject != null)
        {
            AudioSource BGMAudioSrc = BGMAudioObject.GetComponent<AudioSource>();
            if (!BGMAudioSrc.mute)
                BGMAudioSrc.mute = true;
            else
                BGMAudioSrc.mute = false;
        }
    }
    // Getter method for other scripts to access the global toggle values
    public static float GetGlobalToggleValue()
    {
        return globalDayNightToggleValue;
    }

    public static float GetGlobalFogToggleValue()
    {
        return globalFogToggleValue;
    }
}
