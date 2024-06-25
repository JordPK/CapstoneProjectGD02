using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] GameObject mainMenuScreen, newGameScreen, mainMenuOptionsScreen, resourceTrackers, fastForwardSpeedDisplay,optionsScreen;
    [SerializeField] GameObject crewInventoryUI, storageInventoryUI, confirmButton, confirmEjectButton, itemCountButtons, inventoryScreen;
    private GameObject crewMember, storage, airlock;
    public int gameSceneIndex;
    public bool isMainLevel;
    private int fastForwardIndex = 3;
    private int itemCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        //-------------DELETE THIS FOR THE ACTUAL INTEGRATION WHEN NOT TESTING-------------
        StartCoroutine(UpdateResourceCount());
    }

    // Update is called once per frame
    void Update()
    {
        //-------------UNCOMMENT THIS FOR THE ACTUAL INTEGRATION WHEN NOT TESTING-------------
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
        int[] resourceCounts = new int[5];
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

        yield return new WaitForSeconds(2);
        StartCoroutine(UpdateResourceCount());
    }
   

    public void PauseGame()
    {
        //access the game manager to adjust the time scale to zero
    }

    public void PlayGame()
    {
        //access the game manager to adjust the time scale to one
    }

    public void FastForward()
    {
        int fastForwardSpeed = 2 + fastForwardIndex % 3;
        fastForwardSpeedDisplay.GetComponent<TMP_Text>().text = $"x{fastForwardSpeed}";
        //link with a function in the gmae manager that sets the time scale to the fast forward speed's value
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
}
