using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public int startingRoomCount;
    public List<string> goodEventsPool, badEventsPool = new List<string>();

    public static EventManager Instance;

    public RoomEventData FoodRoomEvents;
    public RoomEventData AmmoRoomEvents;
    public RoomEventData GenRoomEvents;
    public RoomEventData ShieldRoomEvents;
    public RoomEventData WaterRoomEvents;
    public RoomEventData CockpitRoomEvents;


    public string generatedEvent;

    public List<Crew> allCrew = new List<Crew>();
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
        allCrew = FindObjectsOfType<Crew>().ToList();

        startingRoomCount = FindObjectsOfType<Room>().Length;
        AddBadToPool(FoodRoomEvents);
        GenerateBadEvents();
        Debug.Log("generated Event " + generatedEvent);
        checkEventType(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGoodToPool(RoomEventData toAdd )
    {
        for (int i = 0; i < toAdd.goodEvents.Count; i++)
        {
            goodEventsPool.Add(toAdd.goodEvents[i]);
            
        }
    } 
    public void AddBadToPool(RoomEventData toAdd )
    {
        for (int i = 0; i < toAdd.badEvent.Count; i++)
        {
            badEventsPool.Add(toAdd.badEvent[i]);
            
        }
    }

    public void RemoveBadFromPool(RoomEventData toRemove)
    {
        for (int i = 0; i < toRemove.badEvent.Count; i++)
        {
            if (badEventsPool.Contains(toRemove.badEvent[i]))
            {
                badEventsPool.RemoveAt(i);
            }
        }
    }
    public void RemoveGoodFromPool(RoomEventData toRemove)
    {
        for (int i = 0; i < toRemove.goodEvents.Count; i++)
        {
            if (goodEventsPool.Contains(toRemove.goodEvents[i]))
            {
                goodEventsPool.RemoveAt(i);
            }
        }
    }

    public string GenerateGoodEvents()
    {
        generatedEvent = goodEventsPool[Random.Range(0, goodEventsPool.Count)];
        return generatedEvent;
    }
    public string GenerateBadEvents()
    {
        generatedEvent = badEventsPool[Random.Range(0, badEventsPool.Count)];
        //ui manager.instance.eventText.text = generatedEvent; 
        return generatedEvent;
    }




    public void checkEventType(bool playerChoice)
    {
        
        if (generatedEvent.Contains("stoners"))
        {
            spaceStoners(playerChoice);

            Debug.Log("Removed food from if else check event type function");
        }
        else if (generatedEvent.Contains(""))
        {
            
        }
        else if (generatedEvent.Contains(""))
        {
           
        }
        else if (generatedEvent.Contains(""))
        {
           
        }
        else
        {
            // Default case or handle unknown event types
        }
        


    }
    public void spaceStoners(bool playerchoice)
    {
        Debug.Log("inside new space stoner function");
        if (playerchoice) ResourceManager.Instance.food -= 25;
        
        else allCrew.RemoveAt(Random.Range(0, allCrew.Count));
    }
   








    public float GetPercentage()
    {
        return (FindObjectsOfType<Room>().Length / startingRoomCount) * 100; 
    }


}
