using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agent : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectedMaterial;
    private Material[] defaultMaterials = new Material[3];
    private NavMeshAgent _agent;
    private SkinnedMeshRenderer _renderer;
    private Material[] newMaterials = new Material[3];
    public bool isMouseOver;
    public bool isMouseDown;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _renderer = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        defaultMaterials = _renderer.materials;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
    }

    private void OnMouseOver()
    {
        if (!CameraManager.Instance.isFirstPerson)
        {
            //turn on highlight colour
            isMouseOver = true;
            if (isMouseDown == false)
            {
                newMaterials[0] = highlightMaterial;
                newMaterials[1] = highlightMaterial;
                newMaterials[2] = highlightMaterial;
                _renderer.materials = newMaterials;
            }
        }
        
       
    }


    private void OnMouseExit()
    {
        // Turn off highlight colour
        isMouseOver = false;
        if(isMouseDown == false)
        {
            _renderer.materials = defaultMaterials;
        }
    }

    private void OnMouseDown()
    {

        if (!CameraManager.Instance.isFirstPerson)
        {
            // Turns on selected colour
            newMaterials[0] = selectedMaterial;
            newMaterials[1] = selectedMaterial;
            newMaterials[2] = selectedMaterial;
            _renderer.materials = newMaterials;

            CrewManager.instance.selectUnit(transform);
            isMouseDown = true;
        }   
    }

    public void UnselectUnit()
    {
        _renderer.materials = defaultMaterials;
        isMouseDown = false;
    }

    public void Animation()
    {
       if(_agent.velocity != Vector3.zero)
        {
            anim.SetInteger("AnimationPar", 1);
            Debug.Log("is moving");
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
            
        }
    }
}
