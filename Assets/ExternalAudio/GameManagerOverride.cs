using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManagerOverride : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip customClip; 

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Overriding Inspector settings in code
        audioSource.clip = null;// customClip;
        audioSource.playOnAwake = true;  // Ensure it doesn't play automatically
        audioSource.loop = true;        // Override to loop the audio
        //audioSource.volume = 0.0f;      // Override volume to 80%
        //audioSource.pitch = 1.2f;       // Override pitch (makes it higher)
    }

    public void setAudioClip(AudioClip aClip)
    {
        customClip = aClip;
        audioSource.clip = customClip;

        playClip();
    }

    private void playClip()
    {
        audioSource.Play();
    }
}