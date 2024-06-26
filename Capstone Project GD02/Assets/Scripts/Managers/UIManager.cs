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

    [Header("HUD References")]
    [SerializeField] GameObject resourceTrackers, fastForwardSpeedDisplay, optionsScreen, crewCount;
    [SerializeField] Slider masterSlider, musicSlider, sfxSlider, uiSfxSlider, fpsSlider, tdSlider;

    [Header("Inventory Management References")]
    [SerializeField] GameObject crewInventoryUI, storageInventoryUI, confirmButton, confirmEjectButton, itemCountButtons, inventoryScreen;

    [Header("Event References")]
    [SerializeField] GameObject eventScreen, eventBodyText;

    [Header("Game Over and Victory References")]
    [SerializeField] GameObject gameOverScreen, gameOverBodyText, victoryScreen, victoryBodyText;

    private GameObject crewMember, storage, airlock;
    private int fastForwardIndex = 3;
    private int itemCount = 1;
    public int[] resourceCounts = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateResourceCount());

        //sets the slider values from player prefs on first load
        //MASTER
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 1);
        AudioMixerManager.Instance.masterVolume = -50 * masterSlider.value;
        //BGM
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1);
        AudioMixerManager.Instance.bgmVolume = -50 * musicSlider.value;
        //SFX
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 1);
        AudioMixerManager.Instance.sfxVolume = -50 * sfxSlider.value;
        //UI
        uiSfxSlider.value = PlayerPrefs.GetFloat("UISFXVol", 1);
        AudioMixerManager.Instance.uiVolume = -50 * uiSfxSlider.value;
        //FPS SENS
        fpsSlider.value = PlayerPrefs.GetFloat("FPSSens", 1);
        //link to camera controller script
        //TD SENS
        tdSlider.value = PlayerPrefs.GetFloat("TDSens", 1);
        //link to camera controller script

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
        yield return new WaitForSeconds(2);
        StartCoroutine(UpdateResourceCount());
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
        AudioMixerManager.Instance.masterVolume = -50 * masterSlider.value;
    }
    public void UpdateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        AudioMixerManager.Instance.bgmVolume = -50 * musicSlider.value;
    }
    public void UpdateSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
        AudioMixerManager.Instance.sfxVolume = -50 * sfxSlider.value;
    }
    public void UpdateUISFXVolume()
    {
        PlayerPrefs.SetFloat("UISFXVol", uiSfxSlider.value);
        AudioMixerManager.Instance.uiVolume = -50 * uiSfxSlider.value;
    }
    public void FPSSensitivity()
    {
        PlayerPrefs.SetFloat("FPSSens", fpsSlider.value);
        //link to camera controller script
    }
    public void TDSensitivity()
    {
        PlayerPrefs.SetFloat("TDSens", tdSlider.value);
        //link to camera controller script
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
        GameManager.Instance.SetTimescale(0f);
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
        GameManager.Instance.SetTimescale(1f);
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
        eventBodyText.GetComponent<TMP_Text>().text = bodyText;
        eventScreen.SetActive(true);
        GameManager.Instance.SetTimescale(0f);
    }

    public void EventOption1Button()
    {
        EventManager.Instance.checkEventType(true);
        eventScreen.SetActive(false);
        GameManager.Instance.SetTimescale(1f);
    }
    public void EventOption2Button()
    {
        EventManager.Instance.checkEventType(false);
        eventScreen.SetActive(false);
        GameManager.Instance.SetTimescale(1f);
    }

    #endregion


    #region Game Over Screens

    public void GameOverScreen()
    {
        gameOverBodyText.GetComponent<TMP_Text>().text = $"Unlucky!\r\n\r\nYour grade for this run was: {ScoreEvaluation.Instance.grade}";
        gameOverScreen.SetActive(true);
    }

    public void VictoryScreen()
    {
        victoryBodyText.GetComponent<TMP_Text>().text = $"Congratulations on surviving for 10 days!\r\n\r\nYour grade for this run was: {ScoreEvaluation.Instance.grade}";
        victoryScreen.SetActive(true);
    }
    public void MainMenuButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    #endregion
}
