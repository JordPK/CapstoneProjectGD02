using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    public static AudioMixerManager Instance { get; private set; }

    AudioMixer mixer;

    //Add new mixer groups here
    [SerializeField] AudioMixerGroup Master;
    [SerializeField] AudioMixerGroup BGM;
    [SerializeField] AudioMixerGroup SFX;
    [SerializeField] AudioMixerGroup Ui;

    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;
    public float uiVolume;





    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        BGM.audioMixer.SetFloat("MasterVolume", Mathf.Clamp(masterVolume, -100, 0));
        BGM.audioMixer.SetFloat("BGMVolume", Mathf.Clamp(bgmVolume, -100, 0));
        BGM.audioMixer.SetFloat("SFXVolume", Mathf.Clamp(sfxVolume, -100, 0));
        BGM.audioMixer.SetFloat("UIVolume", Mathf.Clamp(uiVolume, -100, 0));
    }
}