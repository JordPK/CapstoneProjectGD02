using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DoorOpen : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    [SerializeField] AudioClip doorSFX;
    [SerializeField] GameObject room;
    NavMeshObstacle navMeshObstacle;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!room.activeSelf)
        {
            navMeshObstacle.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crew" && room.activeSelf || other.tag == "Captain" && room.activeSelf)
        {
            anim.SetBool("character_nearby", true);
            audioSource.PlayOneShot(doorSFX, .5f);
            
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
