using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : Audio
{
    public static UIAudioManager Instance;

    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    public override void PlayAudio()
    {
        base.PlayAudio();
    }
}
