using System;
using UnityEngine;

public class DayNightToggle : MonoBehaviour
{
    [SerializeField] GameObject DayMusic;
    [SerializeField] GameObject NightMusic;
    [SerializeField] Light DirectionalLight;

    // Static variable to store the toggle value globally
    private static float globalDayNightToggleValue = 0.0f;
    // Reference to current BGM
    private GameObject BGMAudioObject;

    void Start()
    {
        globalDayNightToggleValue = 0.0f;
        // Start as Day and play BGM
        ToggleDayNight();
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
    }

    void ToggleDayNight()
    {
        // Toggle the global value between 0 and 1
        globalDayNightToggleValue = (globalDayNightToggleValue > 0.5f) ? 0.0f : 1.0f;

        // Find all objects using the DayNightShader and update the toggle value
        DayNightShader[] dayNightShaders = FindObjectsOfType<DayNightShader>();
        foreach (DayNightShader shader in dayNightShaders)
        {
            Debug.Log(shader);
            Debug.Log(globalDayNightToggleValue);
            shader.SetToggleValue(globalDayNightToggleValue);
        }

        EnemyAI enemy = GameObject.FindFirstObjectByType<EnemyAI>();

        if (BGMAudioObject != null)
            PlaySfx.StopLoopedAudio(BGMAudioObject);

        if (globalDayNightToggleValue == 1.0f && enemy != null)
        {
            BGMAudioObject = enemy.SetEnemyBGM(DayMusic);
            DirectionalLight.intensity = 1.0f;
        }
        else if (globalDayNightToggleValue == 0.0f && enemy != null)
        {
            BGMAudioObject = enemy.SetEnemyBGM(NightMusic);
            DirectionalLight.intensity = 0.1f;
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

    public GameObject GetBGMTrack() {
        return globalDayNightToggleValue == 1.0f ? DayMusic : NightMusic;
    }
}
