using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class TankExplosionAudioOverride : MonoBehaviour
{
    private AudioSource audioSource;

    //private AudioClip customClip;
    private AudioClip tankAudioExplosion_AudioClip;

    private AudioManager myAudioManager;

    private void Awake()
    {
        // Get reference to AudioManager
        myAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Overriding Inspector settings in code
        audioSource.clip = null;

        tankAudioExplosion_AudioClip = myAudioManager.getExternalAudio_tankAudioExplosion();
        setAudioClip_TankExplosion(tankAudioExplosion_AudioClip);
    }

    public void setAudioClip(AudioClip aClip)
    {
       //customClip = aClip;
        audioSource.clip = aClip;
    }

    private void setAudioClip_TankExplosion(AudioClip aClip)
    {
        audioSource.clip = aClip;
    }
}