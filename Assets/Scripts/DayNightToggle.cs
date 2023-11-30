using System;
using UnityEngine;

public class DayNightToggle : MonoBehaviour
{
    [SerializeField] GameObject DayMusic;
    [SerializeField] GameObject NightMusic;
    [SerializeField] Light DirectionalLight;
    [SerializeField] GameObject fogCube;

    // Static variable to store the toggle value globally
    private static float globalDayNightToggleValue = 0.0f;
    // Reference to current BGM
    private GameObject BGMAudioObject;
    private bool isFogOn;

    void Start()
    {
        // Start as Day and play BGM
        ToggleDayNight();
        // Start with no fog
        isFogOn = false;
    }

    void Update()
    {
        // Check for user input to toggle day/night
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDayNight();
        }
        // Check for user input to toggle BGM on/off
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleBGM();
        }

        // Check for user input to toggle fog
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFog();
        }
        if (BGMAudioObject != null)
        {
            AudioSource BGMAudioSrc = BGMAudioObject.GetComponent<AudioSource>();
            if (isFogOn)
                BGMAudioSrc.volume = 0.5f;
            else
                BGMAudioSrc.volume = 1f;
        }
    }

    void ToggleDayNight()
    {
        // Toggle the global value between 0 and 1
        globalDayNightToggleValue = (globalDayNightToggleValue > 0.5f) ? 0.0f : 1.0f;

        // Find all objects using the DayNightShader and update the toggle value
        DayNightShader[] dayNightShaders = FindObjectsOfType<DayNightShader>();
        foreach (DayNightShader shader in dayNightShaders)
        {
            shader.SetToggleValue(globalDayNightToggleValue);
        }

        if (BGMAudioObject != null)
            PlaySfx.StopLoopedAudio(BGMAudioObject);

        if (globalDayNightToggleValue == 1.0f)
        {
            BGMAudioObject = PlaySfx.PlayWithLoop(DayMusic, transform);
            DirectionalLight.intensity = 1.0f;
        }
        else if (globalDayNightToggleValue == 0.0f)
        {
            BGMAudioObject = PlaySfx.PlayWithLoop(NightMusic, transform);
            DirectionalLight.intensity = 0.3f;
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

    void ToggleFog()
    {
        // Toggle the fog cube on and off
        fogCube.SetActive(!fogCube.activeSelf);
        isFogOn = !isFogOn;
    }
}
