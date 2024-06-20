using Cinemachine;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private CinemachineVirtualCamera mainCamera;
    void Update()
    {

        
        if (CameraManager.Instance.isFirstPerson)
        {
            mainCamera = CameraManager.Instance.fpsCam;
        }
        else
        {
            mainCamera = CameraManager.Instance.topDownCam;
        }

    }

    void LateUpdate()
    {
    
        transform.LookAt(transform.position + (mainCamera.transform.rotation * Vector3.forward), mainCamera.transform.rotation * Vector3.up);
       
        
    }
}