using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSwitcher : MonoBehaviour
{
    MeshRenderer[] _renderer;

    [SerializeField] Material transparentGlass;
    [SerializeField] Material opaqueGlass;


    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < _renderer.Length; i++)
        {
            if (CameraManager.Instance.isFirstPerson)
            {
                _renderer[i].materials[3] = opaqueGlass;
            }
            else
            {
                _renderer[i].materials[3] = transparentGlass;
            }
        }
       
    }
}
