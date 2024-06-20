using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxManager : MonoBehaviour
{
    [SerializeField] Material skyboxMaterial;

    [SerializeField] float rotationAngle = 0;
    [SerializeField] float skyboxRotationSpeed = 0.1f;

    public Color skyboxColor;


    // Start is called before the first frame update
    void Start()
    {
        skyboxMaterial.SetFloat("_Rotation", rotationAngle);

    }

    // Update is called once per frame
    void Update()
    {
        rotationAngle += skyboxRotationSpeed * Time.deltaTime;
        skyboxMaterial.SetFloat("_Rotation", rotationAngle);
    }
}
