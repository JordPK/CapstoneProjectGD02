using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour
{
    public int maxCrew = 2;
    public List<Crew> crewMembers;
    public bool roomEjected = false;
    public int roomWeight;

    public IndividualInventoryScript inven;
    detachAudio detachAudioSFX;

    void Start()
    {
        crewMembers = new List<Crew>();
        
        detachAudioSFX = FindAnyObjectByType<detachAudio>();
    }
    
    public void AddCrewMember(Crew crewMember)
    {
        if (!crewMembers.Contains(crewMember) && crewMembers.Count < maxCrew)
        {
            crewMembers.Add(crewMember);
            crewMember.ApplyBonus();
            UpdateCrewBonuses();
            Debug.Log("Crew member added: " + crewMember.name);
        }
        else
        {
            Debug.Log("Cannot add crew member, already present or slots are full.");
        }
    }

    public void RemoveCrewMember(Crew crewMember)
    {
        if (crewMembers.Contains(crewMember))
        {
            crewMembers.Remove(crewMember);
            UpdateCrewBonuses();
            Debug.Log("Crew member removed: " + crewMember.name);
        }
        else
        {
            Debug.Log("Crew member not found in the list.");
        }
    }

    void UpdateCrewBonuses()
    {
        /*switch(crewMembers.Count)
        {
           //add varaying bonuses to resource production. 
        }*/
        foreach (Crew crewMember in crewMembers)
        {
            Debug.Log("Bonus applied for crew member: " + crewMember.name);
        }
        gameObject.GetComponent<ResourceGen>().crewMultiplier = 1 - (0.1f * crewMembers.Count);
    }

    public void RoomDetached()
    {
        detachAudioSFX.playDetachSound();

        
        Debug.Log("Room detaching all crew members and dead.");
        ResourceManager.Instance.detectedInvenToRemove(5, 0);

        //Debug.Log(EventManager.Instance.GetPercentage());
        updateEventPool();
        //temporary addtion to save cam sens to proove save will work in with whatever we want lol
        //SaveGameManager.SaveFloat(Application.persistentDataPath + "/CameraSettings.txt", "FPSCamSensitivity", CameraManager.Instance.FPSCamSensitivity);  // NEEDS TO BE FIXED
        // Destroy all crew members in the room
        foreach (Crew crewMember in crewMembers)
        {
            if (crewMember != null)
            {
                Destroy(crewMember.gameObject);
                Debug.Log("Destroyed crew member: " + crewMember.name);
            }
        }
        WeightManager.Instance.rooms.Remove(this);
        WeightManager.Instance.reduceWeight(roomWeight);


        // Clear the crew list
        crewMembers.Clear();

        //check to make sure that the captain hasn't been ejected
        GameManager.Instance.CheckForCaptain();

        // Disable the room game object
        gameObject.SetActive(false);
        roomEjected = true;
    }
    public void jettisonCargo()
    {
        detachAudioSFX.playJettisonSound();
        int[] inven = GetComponent<IndividualInventoryScript>().inventory;
        for (int i = 0; i < inven.Length; i++)
        {
            WeightManager.Instance.removeResourceWeight(inven[i], 1);
            inven[i] = 0;
        }
    }
    public void updateEventPool()
    {
        // Determine the event data based on the room name
        RoomEventData roomEventData = null;

        if (gameObject.name.Contains("AmmoRoom"))
        {
            roomEventData = EventManager.Instance.AmmoRoomEvents;
        }
        else if (gameObject.name.Contains("Cabins"))
        {
            roomEventData = EventManager.Instance.CabinsRoomEvents;
        }
        else if (gameObject.name.Contains("Cockpit"))
        {
            roomEventData = EventManager.Instance.CockpitRoomEvents;
        }
        else if (gameObject.name.Contains("Food"))
        {
            roomEventData = EventManager.Instance.FoodRoomEvents;
        }
        else if (gameObject.name.Contains("GeneratorRoom"))
        {
            roomEventData = EventManager.Instance.GenRoomEvents;
        }
        else if (gameObject.name.Contains("Medbay"))
        {
            roomEventData = EventManager.Instance.MedbayRoomEvents;
        }
        else if (gameObject.name.Contains("ShieldGen"))
        {
            roomEventData = EventManager.Instance.ShieldRoomEvents;
        }
        else if (gameObject.name.Contains("Storage"))
        {
            roomEventData = EventManager.Instance.StorageRoomEvents;
        }
        else if (gameObject.name.Contains("WaterRoom"))
        {
            roomEventData = EventManager.Instance.WaterRoomEvents;
        }
        else
        {
            Debug.LogWarning("No matching room event data found for room: " + gameObject.name);
            return;
        }

        //add and remove events based on each room name 
        EventManager.Instance.AddBadToPool(roomEventData);
        EventManager.Instance.RemoveGoodFromPool(roomEventData);
    }

    public void EjectRoomConfirmation()
    {
        UIManager.Instance.AreYouSure(gameObject);
    }

}

