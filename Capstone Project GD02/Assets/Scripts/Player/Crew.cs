using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crew : MonoBehaviour
{
    public float roomRange = 10f;
    public Room room_;
   
    protected NavMeshAgent agent;
    protected bool hasArrived = false;
    private bool isInRange = false;

    public virtual void ApplyBonus() { }

    public bool IsWithinRoomRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, roomRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Room"))
            {
                Debug.Log("Crew is within room range.");
                return true;
            }
        }
        Debug.Log("Crew is not within room range.");
        return false;
    }

    public void OnArrivedAtDestination()
    {
        Debug.Log("Crew arrived at destination.");
        Room closestRoom = GetClosestRoom(); // Get the closest Room
        room_ = closestRoom; // Assign the Room instance
        if (IsWithinRoomRange())
        {
            room_.AddCrewMember(this);
            Debug.Log("Crew added to Room.");
        }
        else
        {
            room_.RemoveCrewMember(this);
            Debug.Log("Crew is not within room range after arriving.");
        }
    }

    Room GetClosestRoom()
    {
        Room[] rooms = GameObject.FindObjectsOfType<Room>();
        Room closestRoom = null;
        float closestDistance = float.MaxValue;
        foreach (Room room in rooms)
        {
            float distance = Vector3.Distance(transform.position, room.transform.position);
            if (distance < closestDistance)
            {
                closestRoom = room;
                closestDistance = distance;
            }
        }
        return closestRoom;
    }
}


