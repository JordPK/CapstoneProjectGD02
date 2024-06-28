using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [Header("MainMenu References")]
    [SerializeField] GameObject mainMenuScreen, newGameScreen, mainMenuOptionsScreen;

    [Header("Tutorial Screens")]
    [SerializeField] GameObject tutorialScreen1, tutorialScreen2;

    [Header("HUD References")]
    [SerializeField] GameObject resourceTrackers, fastForwardSpeedDisplay, optionsScreen, crewCount, timeText, dayText, crosshair, areYouSureScreen;
    [SerializeField] Slider masterSlider, musicSlider, sfxSlider, uiSfxSlider, fpsSlider, tdSlider;
    [SerializeField] TMP_Text weightGoal, ejectedWeight;

    [Header("Inventory Management References")]
    [SerializeField] GameObject crewInventoryUI, storageInventoryUI, confirmButton, confirmEjectButton, itemCountButtons, inventoryScreen;

    [Header("Event References")]
    [SerializeField] GameObject eventScreen, eventBodyText;

    [Header("Game Over and Victory References")]
    [SerializeField] GameObject gameOverScreen, gameOverBodyText, victoryScreen, victoryBodyText;

    private GameObject ejectReference;
    private GameObject crewMember, storage, airlock;
    private int fastForwardIndex = 3;
    private int itemCount = 1;
    public int[] resourceCounts = new int[5];
    public bool eventActive;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateResourceCount());
        StartCoroutine(UpdateTime());

        //sets the slider values from player prefs on first load
        //MASTER
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 2);
        AudioMixerManager.Instance.masterVolume = -80 + (40 * masterSlider.value);
        //BGM
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 2);
        AudioMixerManager.Instance.bgmVolume = -80 + (40 * musicSlider.value);
        //SFX
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 2);
        AudioMixerManager.Instance.sfxVolume = -80 + (40 * sfxSlider.value);
        //UI
        uiSfxSlider.value = PlayerPrefs.GetFloat("UISFXVol", 2);
        AudioMixerManager.Instance.uiVolume = -80 + (40 * uiSfxSlider.value);
        //FPS SENS
        fpsSlider.value = PlayerPrefs.GetFloat("FPSSens", 1);
        CameraManager.Instance.FPSCamSensitivity = 100 * fpsSlider.value;
        //TD SENS
        tdSlider.value = PlayerPrefs.GetFloat("TDSens", 1);
        CameraManager.Instance.cameraMoveSpeed = 30 * tdSlider.value;


        tutorialScreen1.SetActive(true);
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        //-------------IGNORE THIS, RYAN MIGHT NEED IT LATER BUT PROBABLY NOT-------------
        /*if(SceneManager.GetActiveScene().buildIndex == gameSceneIndex && !isMainLevel)
        {
            isMainLevel = true;
            StartCoroutine(UpdateResourceCount());
        }
        else if(SceneManager.GetActiveScene().buildIndex == gameSceneIndex && isMainLevel)
        {

        }
        else
        {
            isMainLevel = false;
        }*/
    }

    #region MainMenu
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
    #endregion



    #region HUD
    IEnumerator UpdateResourceCount()
    {
        resourceCounts[0] = 0;
        resourceCounts[1] = 0;
        resourceCounts[2] = 0;
        resourceCounts[3] = 0;
        resourceCounts[4] = 0;
        IndividualInventoryScript[] allInventories = FindObjectsOfType<IndividualInventoryScript>();
        //include code that runs through all inventories in the game and sums up the resource values
        foreach (IndividualInventoryScript inventory in allInventories)
        {
            for (int i = 0; i < inventory.inventory.Length; i++)
            {
                resourceCounts[i] += inventory.inventory[i];
            }
        }
        TMP_Text[] resourceText = resourceTrackers.GetComponentsInChildren<TMP_Text>();
        for(int i = 0; i < resourceText.Length; i++)
        {
            
            resourceText[i].text = resourceCounts[i].ToString();
        }
        Crew[] crewCountArray = FindObjectsOfType<Crew>();
        crewCount.GetComponent<TMP_Text>().text = crewCountArray.Length.ToString();

        weightGoal.text = $"Weight Goal: {WeightManager.Instance.targetWeightLoss} kg";
        ejectedWeight.text = $"Total Ejected Weight: {WeightManager.Instance.totalEjectedWeight} kg";

        yield return new WaitForSeconds(2);
        StartCoroutine(UpdateResourceCount());
    }

    IEnumerator UpdateTime()
    {
        int hrs = 7 + Mathf.FloorToInt((TimeManager.Instance.currentTime / TimeManager.Instance.dayDuration) * 14);
        
        timeText.GetComponent<TMP_Text>().text = $"{hrs}:00";
        dayText.GetComponent<TMP_Text>().text = $"Day {GameManager.Instance.currentDay}";

        yield return new WaitForSeconds(1);
        StartCoroutine(UpdateTime());
    }
   

    public void PauseGame()
    {
        //access the game manager to adjust the time scale to zero
        GameManager.Instance.SetTimescale(0f);
    }

    public void PlayGame()
    {
        //access the game manager to adjust the time scale to one
        GameManager.Instance.SetTimescale(1f);
    }

    public void FastForward()
    {
        int fastForwardSpeed = 2 + fastForwardIndex % 3;
        fastForwardSpeedDisplay.GetComponent<TMP_Text>().text = $"x{fastForwardSpeed}";
        //link with a function in the gmae manager that sets the time scale to the fast forward speed's value
        GameManager.Instance.SetTimescale(fastForwardSpeed);
        fastForwardIndex++;
    }

    public void HUDOptionsButton()
    {
        optionsScreen.SetActive(true);
        PauseGame();
    }

    public void HUDOptionsCloseButton()
    {
        optionsScreen.SetActive(false);
        PlayGame();
    }

    public void UpdateMasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
        AudioMixerManager.Instance.masterVolume = -80 + (40 * masterSlider.value);
    }
    public void UpdateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        AudioMixerManager.Instance.bgmVolume = -80 + (40 * musicSlider.value);
    }
    public void UpdateSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
        AudioMixerManager.Instance.sfxVolume = -80 + (40 * sfxSlider.value);
    }
    public void UpdateUISFXVolume()
    {
        PlayerPrefs.SetFloat("UISFXVol", uiSfxSlider.value);
        AudioMixerManager.Instance.uiVolume = -80 + (40 * uiSfxSlider.value);
    }
    public void FPSSensitivity()
    {
        PlayerPrefs.SetFloat("FPSSens", fpsSlider.value);
        CameraManager.Instance.FPSCamSensitivity = 100 * fpsSlider.value;
    }
    public void TDSensitivity()
    {
        PlayerPrefs.SetFloat("TDSens", tdSlider.value);
        CameraManager.Instance.cameraMoveSpeed = 30 * tdSlider.value;
    }


    public void ToggleCrosshair(bool active)
    {
        crosshair.SetActive(active);
    }

    public void TutorialNextPage()
    {
        tutorialScreen1.SetActive(false);
        tutorialScreen2.SetActive(true);
    }

    public void TutorialClose()
    {
        tutorialScreen2.SetActive(false);
        PlayGame();
    }


    #endregion


    #region Inventory

    //----------------------------------------INVENTORY UI----------------------------------------
    public void ChangeItemCount(int value)
    {
        itemCount = value;
    }

    //updates the inventory with the copied values and displays it
    public void PopulateInventory(GameObject tempCrewMember, GameObject tempStorage)
    {
        crewMember = tempCrewMember;
        storage = tempStorage;
        //calls the inventory manager function that copies the inventories over to a temporary array
        InventoryManager.Instance.LoadAndCopyInventories(crewMember, storage);
        UpdateInventoryUI();

        //sets the inventory screen to active
        inventoryScreen.SetActive(true);
        confirmButton.SetActive(true);
        PauseGame();
    }

    public void ShowEjection()
    {
        InventoryManager.Instance.LoadAndCopyInventories(crewMember, airlock);
        UpdateInventoryUI();

        inventoryScreen.SetActive(true);
        confirmEjectButton.SetActive(true);
    }
    //controls the crew to storage transfers when the player clicks on the left hand side inventory
    public void CrewToStorage(int itemIndex)
    {
        InventoryManager.Instance.SubtractItemFromCrew(itemIndex, itemCount);
        UpdateInventoryUI();
    }

    //controls the storage to crew transfers when the player clicks on the right hand side inventory
    public void StorageToCrew(int itemIndex)
    {
        InventoryManager.Instance.AddItemToCrew(itemIndex, itemCount);
        UpdateInventoryUI();
    }

    //confirms the transfer by calling the inventory manager script that sets the actual inventories to their temporary ones
    public void ConfirmTransfer()
    {
        InventoryManager.Instance.ConfirmTransfer(crewMember, storage);

        inventoryScreen.SetActive(false);
        confirmButton.SetActive(false);
        PlayGame();
    }

    

    //updates the UI inventory numbers to reflect the temporary inventories in the inventory manager
    private void UpdateInventoryUI()
    {
        TMP_Text[] crewSlots = crewInventoryUI.GetComponentsInChildren<TMP_Text>();
        for (int i = 0; i < crewSlots.Length; i++)
        {
            crewSlots[i].text = InventoryManager.Instance.temporaryCrewInventory[i].ToString();
        }

        TMP_Text[] storageSlots = storageInventoryUI.GetComponentsInChildren<TMP_Text>();
        for (int i = 0; i < storageSlots.Length; i++)
        {
            storageSlots[i].text = InventoryManager.Instance.temporaryStorageInventory[i].ToString();
        }
    }

    #endregion


    #region Random Events


    public void GetEventDescription(string bodyText)
    {
        string temp = bodyText;
        temp = temp.Replace("\\n", "\n");
        eventBodyText.GetComponent<TMP_Text>().text = temp;
        eventActive = true;
        eventScreen.SetActive(true);
        if (CameraManager.Instance.isFirstPerson)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        PauseGame();
    }

    public void EventOption1Button()
    {
        eventActive = false;
        EventManager.Instance.checkEventType(true);
        eventScreen.SetActive(false);
        if (CameraManager.Instance.isFirstPerson)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        PlayGame();
    }
    public void EventOption2Button()
    {
        eventActive = false;
        EventManager.Instance.checkEventType(false);
        eventScreen.SetActive(false);
        if (CameraManager.Instance.isFirstPerson)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        PlayGame();
    }

    #endregion

    #region Ejecting Rooms

    public void AreYouSure(GameObject roomReference)
    {
        ejectReference = roomReference;
        areYouSureScreen.SetActive(true);
        PauseGame();
    }

    public void YesButton()
    {
        ejectReference.GetComponent<Room>().RoomDetached();
        areYouSureScreen.SetActive(false);
        PlayGame();
    }

    public void NoButton()
    {
        ejectReference = null;
        areYouSureScreen.SetActive(false);
        PlayGame();
    }


    #endregion


    #region Game Over Screens

    public void GameOverScreen()
    {
        gameOverBodyText.GetComponent<TMP_Text>().text = $"Unlucky!\r\n\r\nYour grade for this run was: {ScoreEvaluation.Instance.grade}";
        gameOverScreen.SetActive(true);
        PauseGame();
    }

    public void VictoryScreen()
    {
        victoryBodyText.GetComponent<TMP_Text>().text = $"Congratulations on surviving for 10 days!\r\n\r\nYour grade for this run was: {ScoreEvaluation.Instance.grade}";
        victoryScreen.SetActive(true);
        PauseGame();
    }
    public void MainMenuButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    #endregion
}
