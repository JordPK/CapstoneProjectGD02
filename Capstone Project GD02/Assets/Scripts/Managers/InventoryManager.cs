using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private int[] temporaryCrewInventory = new int[5];
    private int[] temporaryStorageInventory = new int[5];

    //assigns temporary copies of the interacting crew member and storage's inventories so changes can be abandoned at any time
    public void LoadAndCopyInventories(Transform crewMember, Transform storage) //crew member and storage transforms are passed through by the script that calls the method with references to the crew member making the interaction and the storage container in question
    {
        temporaryCrewInventory = crewMember.GetComponent<IndividualInventoryScript>().individualInventory.inventory;
        temporaryStorageInventory = storage.GetComponent<IndividualInventoryScript>().individualInventory.inventory;
    }

    //subtracts the item from the temporary storage inventory and adds it to the temporary crew member's inventory
    public void AddItemToCrew(int itemIndex, int itemCount) //item index is dependent on the UI button being pressed and item count can be scaled using the quanity modifier in the centre
    {
        temporaryCrewInventory[itemIndex] += itemCount;
        temporaryStorageInventory[itemIndex] -= itemCount;
        //call an update function for the UI so that changed are reflected for the player
    }

    //subtracts the item from the temporary crew member's inventory and adds it to the temporary storage inventory
    public void SubtractItemFromCrew(int itemIndex, int itemCount)
    {
        temporaryCrewInventory[itemIndex] -= itemCount;
        temporaryStorageInventory[itemIndex] += itemCount;
    }

    //called when the confirm button is pressed
    public void ConfirmTransfer(Transform crewMember, Transform storage)
    {
        crewMember.GetComponent<IndividualInventoryScript>().individualInventory.inventory = temporaryCrewInventory;
        storage.GetComponent<IndividualInventoryScript>().individualInventory.inventory = temporaryStorageInventory;
    }
}
