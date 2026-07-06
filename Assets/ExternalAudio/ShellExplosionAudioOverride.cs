using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShellExplosionAudioOverride : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip customClip;

    private AudioClip shellAudioExplosion_AudioClip;

    public AudioManager myAudioManager;

    private void Awake()
    {
        // Get reference to AudioManager
        myAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Overriding Inspector settings in code
        audioSource.clip = null;// customClip;
    }

    private void Start()
    {
        shellAudioExplosion_AudioClip = myAudioManager.getExternalAudio_shellAudioExplosion();
        setAudioClip_ShellExplosion(shellAudioExplosion_AudioClip);
    }
    public void setAudioClip(AudioClip aClip)
    {
        customClip = aClip;
        audioSource.clip = customClip;
    }
    private void setAudioClip_ShellExplosion(AudioClip aClip)
    {
        audioSource.clip = aClip;
    }
}