using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShellAudioOverride : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip customClip;

    private AudioClip shellAudioExplosion_AudioClip;

    public AudioManager myAudioManager;

    private ShellExplosion shellExplosionScript;

    private void Awake()
    {
        // Get reference to AudioManager
        myAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        shellExplosionScript = GetComponent<ShellExplosion>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Overriding Inspector settings in code
        audioSource.clip = null;// customClip;
        audioSource.playOnAwake = true;  // Ensure it doesn't play automatically
        audioSource.loop = true;        // Override to loop the audio
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
        shellExplosionScript.m_ExplosionAudio.clip = aClip;
        audioSource.clip = aClip;
        //    setInitialAudioClip_tankMoving(tankMovementScript.m_EngineIdling);
    }
}