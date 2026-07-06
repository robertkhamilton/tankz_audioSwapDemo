using UnityEngine;

public class EnableExternalAudio : MonoBehaviour
{
    AudioManager myAudioManager;
    private bool useExternalAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        useExternalAudio = myAudioManager.getUseExternalAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
