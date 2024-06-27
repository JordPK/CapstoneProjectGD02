using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance {  get; private set; }

    [SerializeField] Camera mainCam;
    public CinemachineVirtualCamera topDownCam;
    public CinemachineVirtualCamera fpsCam;

    [SerializeField] Material materialSwitch;

    [Header("Third Person Camera Controls")]
    public float cameraMoveSpeed = 5;
    public float cameraRotateSpeed = 5;
    [SerializeField] float zoomSpeed;
    [SerializeField] float minFOV = 30;
    [SerializeField] float maxFOV = 80f;

    [Header("First Person Camera Controls")]
    public float FPSCamSensitivity;

    [SerializeField] List<GameObject> roofs;
    [SerializeField] List<GameObject> lights;

    [SerializeField] SkinnedMeshRenderer CaptainCharacter;

    Vector3 topDownCameraOffset;

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

        topDownCameraOffset = topDownCam.transform.position - CaptainCharacter.transform.position;

        // finds all roofs on start 
        roofs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Roof"));
        lights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Light"));

        //only changes are here to proove save loading works lmao 
        Debug.Log(Application.persistentDataPath);
        //SaveGameManager.SaveFloat(Application.persistentDataPath + "/CameraSettings.txt", "FPSCamSensitivity", FPSCamSensitivity);
        
        string test = Application.persistentDataPath + "/CameraSettings.txt";
        Debug.Log(test);
        //FPSCamSensitivity = SaveGameManager.LoadFloatValue(test, "FPSCamSensitivity");
        Debug.Log("FPS Sensitvity loaded : " +  FPSCamSensitivity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangePerspective();
            UIManager.Instance.ToggleCrosshair(isFirstPerson);
        }

        FPSCameraDistanceCheck();

    }

    void ChangePerspective()
    {
        if (!isFirstPerson)
        {
            isFirstPerson = true;
            fpsCam.Priority = 2;
            
        }
        else if (isFirstPerson)
        {
            fpsCam.Priority = 0;
            isFirstPerson = false;
        }
    }

    void SetRoofs(bool isActive)
    {
        
        foreach (GameObject roof in roofs)
        {
            roof.SetActive(isActive);
        }
    }

    void SetLights(bool isActive)
    {

        foreach (GameObject light in lights)
        {
            light.GetComponent<MeshRenderer>().enabled = isActive;
        }
    }

    void FPSCameraDistanceCheck()
    {
        if (Vector3.Distance(mainCam.transform.position, fpsCam.transform.position) < 1.5f)
        {
            SetRoofs(true);
            SetLights(true);
            materialSwitch.SetFloat("_Material_Switch", 0);
            CaptainCharacter.enabled = false;
            topDownCam.transform.position = CaptainCharacter.transform.position + topDownCameraOffset;
            
            
        }
        else
        {
            SetRoofs(false);
            SetLights(false);
            CaptainCharacter.enabled = true;
            materialSwitch.SetFloat("_Material_Switch", 1);
        }
    }


}
