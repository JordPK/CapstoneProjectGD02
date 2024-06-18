using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int maxCrew = 2;
    public List<Crew> crewMembers;

    void Start()
    {
        crewMembers = new List<Crew>();
        Debug.Log("Room initialized with max crew: " + maxCrew);
    }

    public void AddCrewMember(Crew crewMember)
    {
        if (crewMembers.Count < maxCrew)
        {
            crewMembers.Add(crewMember);
            crewMember.ApplyBonus();
            UpdateCrewBonuses();
            Debug.Log("Crew member added: " + crewMember.name);
        }
        else
        {
            Debug.Log("Cannot add crew member, slots are full.");
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
    }
}


