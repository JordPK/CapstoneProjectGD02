using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxManager : MonoBehaviour
{

    public static skyboxManager Instance;


    [SerializeField] Material skyboxMaterial;

    [SerializeField] float rotationAngle = 0;
    [SerializeField] float skyboxRotationSpeed = 0.1f;

    [SerializeField] float lerpDuration = 5f;
    [SerializeField] float lerpTime = 0f;

    
    
    Color startColor;

    [Header("Event Skybox Colours")]
    public  Color normalColour;
    public Color nebulaColour;
    public Color ionColour;
    public Color solarWindsColour;
    public Color gravitationalAnomalyColour;


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
        skyboxMaterial.SetColor("_Tint", normalColour);

        skyboxMaterial.SetFloat("_Rotation", rotationAngle);
        StartCoroutine(LerpSkyboxColor(ionColour));


    }

    // Update is called once per frame
    void Update()
    {
        rotationAngle += skyboxRotationSpeed * Time.deltaTime;
        skyboxMaterial.SetFloat("_Rotation", rotationAngle);
    
    }


    void ChangeSkyboxColor(Color colour)
    {
        StartCoroutine(LerpSkyboxColor(colour));
    }


    IEnumerator LerpSkyboxColor(Color colour)
    {
        lerpTime = 0;
        startColor = skyboxMaterial.GetColor("_Tint");
        while (lerpTime < lerpDuration)
        {
            lerpTime += Time.deltaTime;
            float t = lerpTime / lerpDuration;
            skyboxMaterial.SetColor("_Tint", Color.Lerp(startColor, colour, t));

            yield return null;
        }

        


    }
}
