using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance {  get; private set; }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] GameObject mainMenuScreen, newGameScreen, mainMenuOptionsScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void populateInventory()
    {

    }


    //----------------------------CONTINUE GAME FUNCTION----------------------------
    public void ContinueGame(int sceneIndex)
    {
        //load persistent data
        SceneManager.LoadScene(sceneIndex);
    }

    //----------------------------NEW GAME FUNCTIONS----------------------------
    public void NewGameButton()
    {
        mainMenuScreen.SetActive(false);
        newGameScreen.SetActive(true);
    }
    public void NewGameScreenBackButton()
    {
        mainMenuScreen.SetActive(true);
        newGameScreen.SetActive(false);
    }

    //----------------------------OPTIONS FUNCTIONS----------------------------
    public void OptionsMenu()
    {
        mainMenuScreen.SetActive(false);
        mainMenuOptionsScreen.SetActive(true);
    }


    //----------------------------QUIT FUNCTIONS----------------------------
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
