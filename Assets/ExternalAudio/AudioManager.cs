using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour
{
    public bool useExternalAudio;

    public AudioSource audioSource;

    private string folderPath;// = "C:/YourExternalAudioFolder/"; // Set your path

    public string folderName;

    public string backgroundMusic_FileName;
    private AudioClip backgroundMusic_AudioClip;

    public string tankAudioShotFiring_Filename;
    private AudioClip tankAudioShotFiring_AudioClip;

    public string tankAudioShotCharging_Filename;
    private AudioClip tankAudioShotCharging_AudioClip;

    public string tankAudioEngineIdle_Filename;
    private AudioClip tankAudioEngineIdle_AudioClip;

    public string tankAudioEngineDriving_Filename;
    private AudioClip tankAudioEngineDriving_AudioClip;

    public string shellAudioExplosion_Filename;
    private AudioClip shellAudioExplosion_AudioClip;

    public string tankAudioExplosion_Filename;
    private AudioClip tankAudioExplosion_AudioClip;

    public GameManagerOverride gameManagerOverrideScript;
    public TankAudioOverride tankAudioOverrideScript;
    public ShellAudioOverride shellAudioOverrideScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        DownloadExternalAudio();
    }

    public void DownloadExternalAudio()
    {
        StartCoroutine(LoadAudioCoroutine(backgroundMusic_FileName, backgroundMusic_AudioClip, AudioType.WAV, onComplete));
        StartCoroutine(LoadAudioCoroutine(tankAudioShotFiring_Filename, tankAudioShotFiring_AudioClip, AudioType.WAV, onComplete));
        StartCoroutine(LoadAudioCoroutine(tankAudioShotCharging_Filename, tankAudioShotCharging_AudioClip, AudioType.WAV, onComplete));
        StartCoroutine(LoadAudioCoroutine(tankAudioEngineIdle_Filename, tankAudioEngineIdle_AudioClip, AudioType.WAV, onComplete));
        StartCoroutine(LoadAudioCoroutine(tankAudioEngineDriving_Filename, tankAudioEngineDriving_AudioClip, AudioType.WAV, onComplete));
        StartCoroutine(LoadAudioCoroutine(shellAudioExplosion_Filename, shellAudioExplosion_AudioClip, AudioType.WAV, onComplete));
        StartCoroutine(LoadAudioCoroutine(tankAudioExplosion_Filename, tankAudioExplosion_AudioClip, AudioType.WAV, onComplete));
    }

    public bool getUseExternalAudio() { return useExternalAudio; }
    public AudioClip getExternalAudio_Background() { return backgroundMusic_AudioClip; }

    private IEnumerator LoadAudioCoroutine(string fileName, AudioClip fileClip, AudioType fileType, Action<AudioClip> onComplete )
    {
        //Debug.Log("************************");
        //Debug.Log("folderPath = " + getApplicationDataPath());
        folderPath = getApplicationDataPath();
        folderPath = folderPath.Replace("Tank Game_Data/../", folderName); // won't work in editor
        //Debug.Log("folderPath = " + folderPath);
        //Debug.Log("************************");

        // C:\Users\robcc\OneDrive\Desktop\Tankz\_MUSIC_
        //        string path = Path.Combine(folderPath, fileName );
        string path = folderPath + "/" + fileName;
        //Debug.Log("Path = " + path);

        if (!File.Exists(path)) { Debug.LogError("File not found"); yield break; }

        // Prefix with file:// for local loading
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, fileType))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                fileClip = DownloadHandlerAudioClip.GetContent(www);
                fileClip.name = fileName;                               // filename gets stripped off via UnityWebRequest

                onComplete?.Invoke(fileClip);     // Delegate function fires after coroutine completes

                //Debug.Log("SUCCESS fileClip.name = " + fileClip.name);
            }
        }
    }

    void onComplete(AudioClip audioClip)
    {
        AudioClip currentClip = audioClip;

        string currentAudioClipName = currentClip.name;

        Debug.Log("In AudioManager :: LoadAudioCoroutine > onComplete(AudioClip): " + currentAudioClipName +" loaded successfully.");

        if(currentAudioClipName == backgroundMusic_FileName)
        {
            backgroundMusic_AudioClip = currentClip;
            loadBackgroundMusic(backgroundMusic_AudioClip); // Can load file directly here because GameManager is persistent and not instantiated like Tank or Shell
        }
        else if (currentAudioClipName == tankAudioShotFiring_Filename)
        {
            tankAudioShotFiring_AudioClip = currentClip;
        }
        else if (currentAudioClipName == tankAudioShotCharging_Filename)
        {
            tankAudioShotCharging_AudioClip = currentClip;
        }
        else if (currentAudioClipName == tankAudioEngineIdle_Filename)
        {
            tankAudioEngineIdle_AudioClip = currentClip;
        }
        else if (currentAudioClipName == tankAudioEngineDriving_Filename)
        {
            tankAudioEngineDriving_AudioClip = currentClip;
        }
        else if (currentAudioClipName == shellAudioExplosion_Filename)
        {
            shellAudioExplosion_AudioClip = currentClip;
        }
        else if (currentAudioClipName == tankAudioExplosion_Filename)
        {
            tankAudioExplosion_AudioClip = currentClip;
        }
    }

    private void loadBackgroundMusic(AudioClip aClip)
    {
        gameManagerOverrideScript.setAudioClip(aClip);
    }

    private string getApplicationDataPath()
    {
        string path = Application.dataPath;

        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }

        Debug.Log(path);

        return path;
    }
    
    public AudioClip getExternalAudio_backgroundMusic() { return backgroundMusic_AudioClip;  }
    public AudioClip getExternalAudio_tankAudioShotCharging() { return tankAudioShotCharging_AudioClip; }
    public AudioClip getExternalAudio_tankAudioShotFiring() { return tankAudioShotFiring_AudioClip; }
    public AudioClip getExternalAudio_tankAudioEngineIdle() { return tankAudioEngineIdle_AudioClip; }
    public AudioClip getExternalAudio_tankAudioEngineDriving() { return tankAudioEngineDriving_AudioClip; }
    public AudioClip getExternalAudio_shellAudioExplosion() { return shellAudioExplosion_AudioClip; }
    public AudioClip getExternalAudio_tankAudioExplosion() { return tankAudioExplosion_AudioClip; }

}
