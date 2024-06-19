using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GraphicsManager : MonoBehaviour
{

    public static GraphicsManager Instance;

    [SerializeField] UniversalRenderPipelineAsset UrpAsset;
    [SerializeField] UniversalRenderPipelineAsset mobileUrpAsset;






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
        
        // if desktop version run desktop graphics

        // if mobile version run mobile graphics
    }


    void DesktopGraphicsSettings()
    {
        
    }

    void MobileGraphicsSettings()
    {
        
    }
    
}
