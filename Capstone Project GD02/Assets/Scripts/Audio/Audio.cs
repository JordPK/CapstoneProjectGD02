using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void PlayAudio()
    {   
        audioSource.Play();
    }

    public virtual void StopAudio()
    {
        audioSource.Stop();
    }

   
}
