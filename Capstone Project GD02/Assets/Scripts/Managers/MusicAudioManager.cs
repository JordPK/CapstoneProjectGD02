using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioManager : Audio
{
    public static MusicAudioManager Instance;

    [SerializeField] AudioClip[] music;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
