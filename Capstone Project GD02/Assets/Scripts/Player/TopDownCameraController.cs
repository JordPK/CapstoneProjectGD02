using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopDownCameraController : MonoBehaviour
{

    Touch theTouch;
    public Vector3 moveDirection, lookDirection;
    Vector3 touchStart, touchEnd;

    [SerializeField] Image dpad;
    [SerializeField] float dpadRadius = 300;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!CameraManager.Instance.isFirstPerson)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
           // DesktopInput();
            MobileInput();

#elif UNITY_ANDROID
            //TouchInput();
#endif

            transform.Translate(moveDirection * CameraManager.Instance.cameraMoveSpeed * Time.unscaledDeltaTime, Space.World);

        }
        
    }

    void DesktopInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;  
    }
    bool IsPointerOverUIElement()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
    void MobileInput()
    {

        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            dpad.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
            }

            touchEnd = Input.mousePosition;

            float x = touchEnd.x - touchStart.x;
            float y = touchEnd.y - touchStart.y;

            float distance = (touchEnd - touchStart).magnitude;
            float speed = Mathf.Clamp(distance / dpadRadius, 0,CameraManager.Instance.cameraMoveSpeed);

            moveDirection = new Vector3(x, 0, y).normalized * speed;

            if ((touchEnd - touchStart).magnitude > dpadRadius)
            {
                dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
            }
            else
            {
                dpad.transform.position = touchEnd;
            }
        }

        else
        {
            moveDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
        }
    }

    void TouchInput()
    {
        if (Input.touchCount > 0 && !IsPointerOverUIElement())
        {
            dpad.gameObject.SetActive(true);
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStart = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEnd = theTouch.position;

                float x = touchEnd.x - touchStart.x;
                float y = touchEnd.y - touchStart.y;

                float distance = (touchEnd - touchStart).magnitude;
                float speed = Mathf.Clamp(distance / dpadRadius, 0,1); 

                moveDirection = new Vector3(x, 0, y).normalized * speed;

                if ((touchEnd - touchStart).magnitude > dpadRadius)
                {
                    dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
                }
                else
                {
                    dpad.transform.position = touchEnd;
                }
            }
        }
        else
        {
            moveDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
        }
    }
   
}


