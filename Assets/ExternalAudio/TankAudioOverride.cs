using UnityEngine;

public class TankAudioOverride : MonoBehaviour
{
    private AudioSource[] allAudioSources;

    private TankMovement tankMovementScript;
    private TankShooting tankShootingScript;

    private AudioManager myAudioManager;

    private AudioClip tankAudioShotFiring_AudioClip;
    private AudioClip tankAudioShotCharging_AudioClip;
    private AudioClip tankAudioEngineIdle_AudioClip;
    private AudioClip tankAudioEngineDriving_AudioClip;

    void Awake()
    {
        // returns array of AudioSources on this gameObject ordered on vertical stack in Inspector (top to bottom)
        allAudioSources = GetComponents<AudioSource>();

        // references to tank scripts on this gameObject
        tankMovementScript = GetComponent<TankMovement>();
        tankShootingScript = GetComponent<TankShooting>();

        // Get reference to AudioManager
        myAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // Clear out Inspector values when using overrides
        initAudioSources();
    }

    private void initAudioSources()
    {
        // Null and Set properties the AudioSource component for MovementAudio (AudioSource) on TankMovement.cs (allAudioSources[0])
        allAudioSources[0].clip = null;
        allAudioSources[0].playOnAwake = true;  // Ensure it doesn't play automatically
        allAudioSources[0].loop = true;        // Override to loop the audio

        // Null and Set properties the AudioSource component for ShootingAudio (AudioSource) on TankMovement.cs (allAudioSources[1])
        allAudioSources[1].clip = null;
        allAudioSources[1].playOnAwake = false;  // Ensure it doesn't play automatically
        allAudioSources[1].loop = false;        // Override to loop the audio
        //audioSource.volume = 0.0f;      // Override volume to 80%
        //audioSource.pitch = 1.2f;       // Override pitch (makes it higher)
    }

    private void Start()
    {
        tankAudioShotFiring_AudioClip = myAudioManager.getExternalAudio_tankAudioShotFiring();
        tankAudioShotCharging_AudioClip = myAudioManager.getExternalAudio_tankAudioShotCharging();
        tankAudioEngineIdle_AudioClip = myAudioManager.getExternalAudio_tankAudioEngineIdle();
        tankAudioEngineDriving_AudioClip = myAudioManager.getExternalAudio_tankAudioEngineDriving();

        setAudioClip_EngineIdle(tankAudioEngineIdle_AudioClip);
        setAudioClip_EngineDriving(tankAudioEngineDriving_AudioClip);
        setAudioClip_shotCharging(tankAudioShotCharging_AudioClip);
        setAudioClip_shotFiring(tankAudioShotFiring_AudioClip);
    }

    private void setAudioClip_EngineIdle(AudioClip aClip)
    {
        tankMovementScript.m_EngineIdling = aClip;
        setInitialAudioClip_tankMoving(tankMovementScript.m_EngineIdling);
    }

    public void setAudioClip_EngineDriving(AudioClip aClip)
    {
        tankMovementScript.m_EngineDriving = aClip;
    }

    public void setAudioClip_shotCharging(AudioClip aClip)
    {
        tankShootingScript.m_ChargingClip = aClip;
        setInitialAudioClip_tankShooting(tankShootingScript.m_ChargingClip);
    }

    public void setAudioClip_shotFiring(AudioClip aClip)
    {
        tankShootingScript.m_FireClip = aClip;
    }

    private void setInitialAudioClip_tankMoving(AudioClip aClip)
    {
        allAudioSources[0].clip = aClip;
    }
    private void setInitialAudioClip_tankShooting(AudioClip aClip)
    {
        allAudioSources[1].clip = aClip;
    }
}
