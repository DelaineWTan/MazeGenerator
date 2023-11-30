using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySfx : MonoBehaviour
{
    public static void PlayThenDestroy(GameObject soundPrefab, Transform transform)
    {
        // Spawn the sound object
        GameObject m_Sound = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        AudioSource m_Source = m_Sound.GetComponent<AudioSource>();

        float life = m_Source.clip.length / m_Source.pitch;
        Destroy(m_Sound, life);
    }

    public static GameObject PlayWithLoop(GameObject soundPrefab, Transform transform)
    {
        // Spawn the sound object
        GameObject soundObject = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();

        // Set the AudioSource to loop
        audioSource.loop = true;

        return soundObject;
    }

    // Destroy the looped audio object immediately, may cause a slight buzzing due to abruptness
    public static void StopLoopedAudio(GameObject soundObject)
    {
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = false; // Disable loop
            Destroy(audioSource.gameObject); // Destroy after the clip finishes
        }
    }
}
