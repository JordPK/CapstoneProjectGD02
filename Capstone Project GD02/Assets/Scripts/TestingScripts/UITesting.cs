using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UITesting : MonoBehaviour
{
    public GameObject crewMember, storage, airlock, crewInventoryUI, storageInventoryUI, confirmButton, confirmEjectButton;
    public int itemCount = 1;








    //----------------------------------------INVENTORY UI----------------------------------------
    //adds 3 food to the crew member's inventory for testing
    public void AddFood()
    {
        crewMember.GetComponent<IndividualInventoryScript>().inventory[0] += 3;
    }

    //updates the inventory with the copied values and displays it
    public void ShowInventory()
    {
        //calls the inventory manager function that copies the inventories over to a temporary array
        InventoryManager.Instance.LoadAndCopyInventories(crewMember, storage);
        UpdateInventoryUI();

        //sets the inventory screen to active
        crewInventoryUI.SetActive(true);
        storageInventoryUI.SetActive(true);
        confirmButton.SetActive(true);
    }

    public void ShowEjection()
    {
        InventoryManager.Instance.LoadAndCopyInventories(crewMember, airlock);
        UpdateInventoryUI();

        crewInventoryUI.SetActive(true);
        storageInventoryUI.SetActive(true);
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

        crewInventoryUI.SetActive(false);
        storageInventoryUI.SetActive(false);
        confirmButton.SetActive(false);
    }

    public void ConfirmEjection()
    {
        InventoryManager.Instance.ConfirmEjection(crewMember, airlock);

        crewInventoryUI.SetActive(false);
        storageInventoryUI.SetActive(false);
        confirmEjectButton.SetActive(false);
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
}
