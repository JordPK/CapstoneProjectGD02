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

    public int[] temporaryCrewInventory = new int[5];
    public int[] temporaryStorageInventory = new int[5];

    //assigns temporary copies of the interacting crew member and storage's inventories so changes can be abandoned at any time
    public void LoadAndCopyInventories(GameObject crewMember, GameObject storage) //crew member and storage transforms are passed through by the script that calls the method with references to the crew member making the interaction and the storage container in question
    {
        for(int i =0; i< crewMember.GetComponent<IndividualInventoryScript>().inventory.Length; i++)
        {
            temporaryCrewInventory[i] = crewMember.GetComponent<IndividualInventoryScript>().inventory[i];
        }
        for (int i = 0; i < storage.GetComponent<IndividualInventoryScript>().inventory.Length; i++)
        {
            temporaryStorageInventory[i] = storage.GetComponent<IndividualInventoryScript>().inventory[i];
        }
    }

    //subtracts the item from the temporary storage inventory and adds it to the temporary crew member's inventory
    public void AddItemToCrew(int itemIndex, int itemCount) //item index is dependent on the UI button being pressed and item count can be scaled using the quanity modifier in the centre
    {
        //checks to make sure that the item transfer will not result in a number below 0
        if (temporaryStorageInventory[itemIndex] - itemCount >= 0)
        {
            temporaryCrewInventory[itemIndex] += itemCount;
            temporaryStorageInventory[itemIndex] -= itemCount;
        }
    }

    //subtracts the item from the temporary crew member's inventory and adds it to the temporary storage inventory
    public void SubtractItemFromCrew(int itemIndex, int itemCount)
    {
        //checks to make sure that the item transfer will not result in a number below 0
        if (temporaryCrewInventory[itemIndex] - itemCount >= 0)
        {
            temporaryCrewInventory[itemIndex] -= itemCount;
            temporaryStorageInventory[itemIndex] += itemCount;
        }
    }

    //called when the confirm button is pressed
    public void ConfirmTransfer(GameObject crewMember, GameObject storage)
    {
        for(int i = 0; i<temporaryCrewInventory.Length; i++)
        {
            crewMember.GetComponent<IndividualInventoryScript>().inventory[i] = temporaryCrewInventory[i];
        }

        for (int i = 0; i < temporaryStorageInventory.Length; i++)
        {
            storage.GetComponent<IndividualInventoryScript>().inventory[i] = temporaryStorageInventory[i];
        }
    }

    
}
