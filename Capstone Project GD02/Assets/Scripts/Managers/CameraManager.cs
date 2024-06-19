using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance {  get; private set; }

    public CinemachineVirtualCamera topDownCam;
    public CinemachineVirtualCamera fpsCam;

    [Header("Third Person Camera Controls")]
    public float cameraMoveSpeed = 5;
    [SerializeField] float zoomSpeed;
    [SerializeField] float minFOV = 30;
    [SerializeField] float maxFOV = 80f;

    [Header("First Person Camera Controls")]
    public float FPSCamSensitivity;

    [SerializeField] List<GameObject> roofs;

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
    void Start()
    {
        topDownCam.Priority = 1;

        // finds all roofs on start 
        roofs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Roof"));
        
        // Set roofs default state
        SetRoofs(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangePerspective();
        }

    }

    void ChangePerspective()
    {
        if (!isFirstPerson)
        {
            isFirstPerson = true;
            fpsCam.Priority = 2;
            SetRoofs(true);
        }
        else if (isFirstPerson)
        {
            fpsCam.Priority = 0;
            isFirstPerson = false;
            SetRoofs(false);
        }
    }

    void SetRoofs(bool isActive)
    {
        
        foreach (GameObject roof in roofs)
        {
            roof.SetActive(isActive);
        }
    }

}
