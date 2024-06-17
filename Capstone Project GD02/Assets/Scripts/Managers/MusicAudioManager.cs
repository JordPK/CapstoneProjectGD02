using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioManager : Audio
{
    public static MusicAudioManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
