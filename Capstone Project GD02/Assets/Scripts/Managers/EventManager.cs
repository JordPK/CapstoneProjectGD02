using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EventManager : MonoBehaviour
{
    public int startingRoomCount;
    public List<string> goodEventsPool, badEventsPool = new List<string>();

    public static EventManager Instance;

    public RoomEventData AmmoRoomEvents;
    public RoomEventData CabinsRoomEvents;
    public RoomEventData CockpitRoomEvents;
    public RoomEventData FoodRoomEvents;
    public RoomEventData GenRoomEvents;
    public RoomEventData MedbayRoomEvents;
    public RoomEventData ShieldRoomEvents;
    public RoomEventData StorageRoomEvents;
    public RoomEventData WaterRoomEvents;


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
        AddGoodToPool(AmmoRoomEvents);
        AddGoodToPool(CabinsRoomEvents);
        AddGoodToPool(CockpitRoomEvents);
        AddGoodToPool(FoodRoomEvents);
        AddGoodToPool(GenRoomEvents);
        AddGoodToPool(MedbayRoomEvents);
        AddGoodToPool(ShieldRoomEvents);
        AddGoodToPool(StorageRoomEvents);
        AddGoodToPool(WaterRoomEvents);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGoodToPool(RoomEventData toAdd )
    {
        for (int i = 0; i < toAdd.goodEvents.Count; i++)
        {
            if (!goodEventsPool.Contains(toAdd.goodEvents[i])) goodEventsPool.Add(toAdd.goodEvents[i]);
            
        }
    } 
    public void AddBadToPool(RoomEventData toAdd )
    {
        for (int i = 0; i < toAdd.badEvent.Count; i++)
        {
            if (!badEventsPool.Contains(toAdd.badEvent[i])) badEventsPool.Add(toAdd.badEvent[i]);
            
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
            BadSpaceStoners(playerChoice);

            Debug.Log("Removed food from if else check event type function");
        }
        else if (generatedEvent.Contains("hungry."))
        {
            BadSnickers(playerChoice);
        }
        else if (generatedEvent.Contains("spa-ishing"))
        {
            BadFishing(playerChoice);
        }
        else if (generatedEvent.Contains("smoothies"))
        {
            GoodSmoothies(playerChoice);
        }
        else
        {
            // Default case or handle unknown event types
        }
        


    }
    //event functions
    public void BadSpaceStoners(bool playerchoice)
    {
        //There is an out break of space stoners on board, they all have the munchies.
        
        if (playerchoice) RemoveResoruce("food", 25);

        else
        {
            removeRandomCrew(1);
        }
    }
    public void BadSnickers(bool playerchoice)
    {
        //You’re not yourself when you are hungry.
        if (playerchoice)
        {
            RemoveResoruce("food", 5);
            ResourceManager.Instance.detectedInvenToRemove(5, 0);
        }
        else
        {
            // if false stop working
            int a = Random.Range(0, allCrew.Count);
            NavMeshAgent stopWorking = allCrew[a].GetComponent<NavMeshAgent>();
            //randomly gets a crew members nav mesh agent
            //if stops nav mesh agent from working for time period 
            if (stopWorking.destination != null)
            {
                StartCoroutine(pauseWork(stopWorking));
            }
        }
    }
    public void BadFishing(bool playerchoice)
    {
        if(playerchoice) removeRandomCrew(1);
        else { 
            RemoveResoruce("fuel", 10);
            RemoveResoruce("medicalSupplies", 10);
        }
    }
    public void GoodSmoothies(bool playerchoice)
    {
        //Your crew, out of sheer despiration created space smoothies combining powered nutrietns and recycled water. the past can be injested for a full but light feeling.
        if (playerchoice)
        {
            // if true speed boost random crew member
            int a = Random.Range(0, allCrew.Count);
            NavMeshAgent boost = allCrew[a].GetComponent<NavMeshAgent>();
            StartCoroutine(speedBoost(boost, 10));
        }

        else
        {
            int a = Random.Range(0, allCrew.Count);
            NavMeshAgent boost = allCrew[a].GetComponent<NavMeshAgent>();
            StartCoroutine(speedBoost(boost, 10));
        }


    }


    //generic event action functions 
    public void removeRandomCrew(int crewToRemove)
    {
        for (int i = 0; i <= crewToRemove; i++)
        {
            int a = Random.Range(0, allCrew.Count);
            Destroy(allCrew[a].gameObject);
            allCrew.RemoveAt(a);
        }
    }
    public void RemoveResoruce(string resoruceName, int amount)
    {
        switch (resoruceName.ToLower())
        {
            case "food":
                ResourceManager.Instance.food -= amount;
                WeightManager.Instance.removeResourceWeight(amount, 1);
                break;
            case "water":
                ResourceManager.Instance.water -= amount;
                WeightManager.Instance.removeResourceWeight(amount, 1);
                break;
            case "medicalsupplies":
                ResourceManager.Instance.medicalSupplies -= amount;
                WeightManager.Instance.removeResourceWeight(amount, 3);
                break;
            case "ammo":
                ResourceManager.Instance.ammo -= amount;
                WeightManager.Instance.removeResourceWeight(amount, 2);
                break;
            case "fuel":
                ResourceManager.Instance.fuel -= amount;
                WeightManager.Instance.removeResourceWeight(amount, 2);
                break;
            default:
                break;
        }
    }
    public void AddResoruce(string resoruceName, int amount)
    {
        switch (resoruceName.ToLower())
        {
            case "food":
                ResourceManager.Instance.food += amount;
                break;
            case "water":
                ResourceManager.Instance.water += amount;
                break;
            case "medicalsupplies":
                ResourceManager.Instance.medicalSupplies += amount;
                break;
            case "ammo":
                ResourceManager.Instance.ammo += amount;
                break;
            case "fuel":
                ResourceManager.Instance.fuel += amount;
                break;
            default:
                break;
        }
    }
    IEnumerator pauseWork(NavMeshAgent target)
    {
        //stops movement of navmeshagent 
        target.isStopped = true;
        yield return new WaitForSeconds(60);
        target.isStopped = false;
    }
    IEnumerator speedBoost(NavMeshAgent target, int boostTime)
    {
        float startSpeed = target.speed;

        target.speed *= 1.5f;

        yield return new WaitForSeconds(boostTime);
        target.speed = startSpeed;
    }




    //return % of remaining rooms for post game evaluation
    public float GetPercentage()
    {
        int currRoomCount = FindObjectsOfType<Room>().Length;
        if (startingRoomCount == 0) return 0; 
        return ((float)currRoomCount / startingRoomCount) * 100;
    }
    


}
