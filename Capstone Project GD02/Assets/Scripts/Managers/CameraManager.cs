using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance {  get; private set; }

    [SerializeField] CinemachineVirtualCamera topDownCam;
    //[SerializeField] CinemachineVirtualCamera fpsCam;

    public bool isFirstPerson = false;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangePerspective()
    {

    }
}
