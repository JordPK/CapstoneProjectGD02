using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [Header("Interaction Ranges")]
    float currentInteractionRange;
    [SerializeField] float FPSinteractionRange = 4f;
    [SerializeField] float RTSinteractionRange = 99999f;

    [SerializeField] LayerMask interactableLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CameraManager.Instance.isFirstPerson)
        {
            currentInteractionRange = FPSinteractionRange;
            FindInteractable();
        }
        if (Input.GetMouseButtonDown(0) && CrewManager.instance.selection == null && !CameraManager.Instance.isFirstPerson)
        {
            currentInteractionRange = RTSinteractionRange;
            FindInteractable(); 
        }

    }
    void FindInteractable()
    {

        

        // raycast sent from center of screen because mouse position is locked.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // On Raycast hit check for tags
        if (Physics.Raycast(ray, out hit, currentInteractionRange, interactableLayer))
        {
            switch (hit.transform.tag)
            {
                case "FoodConsole":
                    Debug.Log("You hit food console");
                    break;
                case "WaterConsole":
                    Debug.Log("You hit water console");
                    break;
                case "MedicalConsole":
                    Debug.Log("You hit medical console");
                    break;
                case "AmmoConsole":
                    Debug.Log("You hit ammo console");
                    break;
                case "FuelConsole":
                    Debug.Log("You hit fuel console");
                    break;
            }
           
        }
        else
        {
            Debug.Log("you hit nothing");
        }
    }

}
