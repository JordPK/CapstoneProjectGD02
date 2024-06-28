using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] Footsteps;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
 
    void PlayFootstep(int FootstepsIndex)
    {
        audioSource.PlayOneShot(Footsteps[FootstepsIndex], .6f);
    }
}
