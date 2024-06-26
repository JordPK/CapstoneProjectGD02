using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    [SerializeField] AudioClip doorSFX;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crew" || other.tag == "Captain")
        {
            anim.SetBool("character_nearby", true);
            audioSource.PlayOneShot(doorSFX, .7f);
            
        } 
    }

    private void OnTriggerExit(Collider other)
    {
            if (other.tag == "Crew" || other.tag == "Captain")
            {
                anim.SetBool("character_nearby", false);
                //WeightManager.Instance.rooms.ElementAt(i).roomEjected;
            }
    }

    
}
