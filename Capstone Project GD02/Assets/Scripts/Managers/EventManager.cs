using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public int startingRoomCount;
    public List<string> goodEventsPool, badEventsPool = new List<string>();

    public static EventManager Instance;

    public RoomEventData FoodRoomEvents;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        startingRoomCount = FindObjectsOfType<Room>().Length;
        AddToPool(FoodRoomEvents);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPool(RoomEventData toAdd )
    {
        for (int i = 0; i < toAdd.goodEvents.Count; i++)
        {
            goodEventsPool.Add(toAdd.goodEvents[i]);
            
        }
    }

    public void RemoveFromPool()
    {

    }

    public void GenerateEvents()
    {

    }

    //find out remaining room perentages
    public float GetPercentage()
    {
        return (FindObjectsOfType<Room>().Length / startingRoomCount) * 100; 
    }
}
