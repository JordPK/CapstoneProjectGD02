using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detachAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip detachSFX;
    [SerializeField] AudioClip jettisonSFX;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDetachSound()
    {
        audioSource.PlayOneShot(detachSFX, 1);
    }
    public void playJettisonSound()
    {
        audioSource.PlayOneShot(jettisonSFX, 1);
    }
}
