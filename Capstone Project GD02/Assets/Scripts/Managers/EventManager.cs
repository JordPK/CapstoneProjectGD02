using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class EventManager : MonoBehaviour
{
    #region variables
    public int startingRoomCount;
    public List<string> goodEventsPool, badEventsPool = new List<string>();
    public static EventManager Instance;
    public RoomEventData AirlockRoomEvents;
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
    public int medicalCostMultiplier;
    public int ammoCostMulti;
    public int fuelCostMulti;
    public int foodCostMulti;
    public int waterCostMulti;
    public List<Crew> allCrew = new List<Crew>();

    #endregion
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
        AddGoodToPool(AirlockRoomEvents);
        Debug.Log(AirlockRoomEvents.goodEvents.ElementAt(0));
        Debug.Log("newline test \n test new line in debugs");
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
    
    #region Pools & event generation
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
        int rand = Random.Range(0, goodEventsPool.Count) + 1;
        generatedEvent = goodEventsPool[rand];
        goodEventsPool.RemoveAt(rand);
        return generatedEvent;
    }
    public string GenerateBadEvents()
    {
        int rand = Random.Range(0, badEventsPool.Count) + 1;
        generatedEvent = badEventsPool[rand];
        goodEventsPool.RemoveAt(rand);
        //ui manager.instance.eventText.text = generatedEvent; 
        return generatedEvent;
    }
    #endregion


    //checks and applies effect of generated effect with the player choice 
    public void checkEventType(bool playerChoice)
    {
        #region Room Event Checks
        #region foodEventCheck
        if (generatedEvent.Contains("stoners"))
        {
            BadSpaceStoners(playerChoice);

            
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
        #endregion

        #region medbayEventCheck
        else if (generatedEvent.Contains("breakthrough"))
        {
            GoodBreakthrough();
        }
        else if (generatedEvent.Contains("techniques"))
        {
            GoodNewTechnique();
        }
        else if (generatedEvent.Contains("tetanus"))
        {
            BadTetnus(playerChoice);
        }
        else if (generatedEvent.Contains("amputation"))
        {
            BadAmputation(playerChoice);
        }
        else if (generatedEvent.Contains("zoomed"))
        {
            BadZoom(playerChoice);
        }
        else if (generatedEvent.Contains("hypochondriacs"))
        {
            BadHypochondriacs(playerChoice);
        }
        else if (generatedEvent.Contains("mess-hall"))
        {
            BadMessHall(playerChoice);
        }
        else if (generatedEvent.Contains("concocted"))
        {
            BadGrenade();

        }
        #endregion

        #region AmmoEventCheck
        else if (generatedEvent.Contains("laugh"))
        {
            BadLaugh();
        }
        else if (generatedEvent.Contains("cheese"))
        {
            BadCheese(playerChoice);
        }
        else if (generatedEvent.Contains("scoundrel"))
        {
            GoodRobbery();
        }
        else if (generatedEvent.Contains("stubbing"))
        {
            BadStub(playerChoice);
        }

        #endregion

        #region CabinsEventCheck
        else if (generatedEvent.Contains("yoga"))
        {
            GoodYoga();
        }
        else if (generatedEvent.Contains("hammock"))
        {
            BadHammock(playerChoice);
        }
        #endregion

        #region ShieldEventCheck
        else if (generatedEvent.Contains("moon"))
        {
            BadMoon(playerChoice);
        }
        else if (generatedEvent.Contains("tours"))
        {
            BadTour(playerChoice);
        }
        else if (generatedEvent.Contains("lost"))
        {
            GoodFastAction();
        }
        else if (generatedEvent.Contains("offence"))
        {
            BadDefence();
        }
        else if (generatedEvent.Contains("superstitious"))
        {
            BadSuperstitons(playerChoice);
        }
        else if (generatedEvent.Contains("huddling"))
        {
            BadBlanket(playerChoice);
        }
        #endregion

        #region GeneratorEventChecks
        else if (generatedEvent.Contains("shocking"))
        {
            GoodLick();
        }
        else if (generatedEvent.Contains("blackout"))
        {
            BadBlackout(playerChoice);
        }
        #endregion

        #region WaterEventChecks
        else if (generatedEvent.Contains("longest"))
        {
            GoodSlide();
        }
        else if (generatedEvent.Contains("shocking"))
        {
            GoodLick();
        }
        else if (generatedEvent.Contains("shocking"))
        {
            GoodLick();
        }
        #endregion

        #region Airlock Event Checks
        else if (generatedEvent.Contains("spacewalks"))
        {
            BadSpacewalk(playerChoice);
        }
        else if (generatedEvent.Contains("jettisoning"))
        {
            GoodFeeling();
        }
        #endregion

        #region Cockpit Event Checks
        else if (generatedEvent.Contains("barrel"))
        {
            BadRoll();
        }
        else if (generatedEvent.Contains("derelict"))
        {
            GoodMisfortune();
        }
        #endregion

        #region Storage Event Checks
        else if (generatedEvent.Contains("packer"))
        {
            GoodPacker();
        }
        else if (generatedEvent.Contains("jenga"))
        {
            BadJenga();
        }
        else if (generatedEvent.Contains("extra"))
        {
            GoodExtra();
            
        }
        #endregion

        #endregion
        else
        {
            //default 
        }
    }

    #region Room Event functions 

    #region Airlock
    public void BadSpacewalk(bool playerchoice)
    {
        if (playerchoice) removeRandomCrew(2);
        else RemoveResoruce("medicalsupplies", 10);
    }
    public void GoodFeeling()
    {
        int a = Random.Range(0, allCrew.Count);
        NavMeshAgent boost = allCrew[a].GetComponent<NavMeshAgent>();
        StartCoroutine(speedBoost(boost, 15));
    }
    #endregion

    #region Ammo Room
    public void BadLaugh()
    {
        removeRandomCrew(1);
    }
    public void BadCheese(bool playerchoice)
    {
        if(playerchoice)
        {
            RemoveResoruce("ammo", 5);
        }
        else
        {
            removeRandomCrew(2);
            RemoveResoruce("food", 25);
        }
    }
    public void GoodRobbery()
    {
        
        int randCount = Random.Range(3, 30);
        AddResoruce(randomResrouceSelection(), randCount);
        AddResoruce(randomResrouceSelection(), randCount);
        
    }
    public void BadStub(bool playerchoice)
    {
        if (playerchoice) removeRandomCrew(1);
        else RemoveResoruce("medicalsupplies", 50);

    }

    #endregion

    #region Cabins
    public void GoodYoga()
    {
        StartCoroutine(prodBoost(.4f, 15));

    }
    public void BadHammock(bool playerchoice)
    {
        if (playerchoice) removeRandomCrew(1);
        else
        {
            prodBoost(0.05f, 45);
            foreach(Crew crew in allCrew)
            {
                speedDebuff(crew.GetComponent<NavMeshAgent>(),15 );

            }
            RemoveResoruce("medicalsupplies", 3);
        }
    }
    

    #endregion

    #region Cockpit Room
    public void GoodMisfortune()
    {
        AddResoruce(randomResrouceSelection(), 15);
        AddResoruce(randomResrouceSelection(), 15);
        
    }
    public void BadRoll()
    {
        foreach (var crew in allCrew)
        {
            StartCoroutine(pauseWork(crew.GetComponent<NavMeshAgent>()));
        }
    }


    #endregion
    #region foodRoom
    public void BadSpaceStoners(bool playerchoice)
    {
        //There is an out break of space stoners on board, they all have the munchies.

        if (playerchoice) RemoveResoruce("food", 25);

        else
        {
            removeRandomRoom(1);
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
        if (playerchoice) removeRandomCrew(1);
        else
        {
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
    #endregion
    #region Generator room
    public void GoodLick()
    {
        int a = Random.Range(0, allCrew.Count);
        NavMeshAgent boost = allCrew[a].GetComponent<NavMeshAgent>();
        StartCoroutine(speedBoost(boost, 10));
       
    }

    public void BadBlackout(bool playerchoice)
    {
        if(playerchoice)
        {
            RemoveResoruce("food", 10);
            RemoveResoruce("water", 25);
            RemoveResoruce("fuel", 5);
        } else
        {
            
            foreach (var crew in allCrew)
            {
                speedDebuff(crew.GetComponent<NavMeshAgent>(), 15);
                prodBoost(0.05f, 15);
            }
            RemoveResoruce("medicalsupplies", 5);
            
        }
    }


    #endregion
    #region Medbay
    public void GoodBreakthrough()
    {
        //your medical staff had a brealthrough
        AddResoruce("medicalsupplies", 25);
    }

    public void GoodNewTechnique()
    {
        StartCoroutine(prodBoost(.2f ,20));
    }

    public void BadTetnus(bool playerchoice)
    {
        if (playerchoice)
        {
            removeRandomCrew(1);
        } 
        else
        {
            RemoveResoruce("medicalsupplies", 5);
        }
    }
    public void BadAmputation(bool playerchoice)
    {
        if (playerchoice)
        {
            removeRandomCrew(1);
        }
        else
        {
            RemoveResoruce("medicalsupplies", 1);
        }
    }
    public void BadGrenade()
    {
        removeRandomCrew(2);
    }
    public void BadZoom(bool playerchoice)
    {
        if (playerchoice)
        {
            randomDestination();
        }
        else
        {
            RemoveResoruce("fuel", 2);
            RemoveResoruce("medicalsupplies", 5);
        }
    }
    public void BadHypochondriacs(bool playerchoice)
    {
        if (playerchoice)
        {
            StartCoroutine(prodBoost(.05f, 50));
        }
        else
        {
            RemoveResoruce("medicalsupplies", 5); // lower value suggests placebos given out 
        }
    }
    public void BadMessHall(bool playerchoice)
    {
        medicalCostMultiplier = playerchoice ? 2: 1;
    }

    #endregion
    #region ShieldRoom
    
    public void GoodFastAction()
    {
        
        AddResoruce("medicalsupplies", 5);
        AddResoruce("ammo", 15);
        
    }
    public void BadTour(bool playerchoice)
    {
        AddResoruce("water", 15);
        AddResoruce("food", 10);
    }
    public void BadDefence()
    {
        RemoveResoruce("ammo", 25);
        
    }
    public void BadMoon(bool playerchoice)
    {
        if (playerchoice) RemoveResoruce("medicalsupplies", 15);
        else removeRandomRoom(1);
    }
    public void BadSuperstitons(bool playerchoice)
    {
        if (playerchoice)
        {

            foreach (var crew in allCrew)
            {
                speedDebuff(crew.GetComponent<NavMeshAgent>(), 15);

            }
        }
        else
        {
            // superstituous riots led to the crew starting fires
            RemoveResoruce("fuel", 25);
        }
    }
    public void BadBlanket(bool playerchoice)
    {
        //randomly stops 3 crew from working 
        for (int i = 0; i <= 3; i++) {
            int a = Random.Range(0, allCrew.Count);
            NavMeshAgent stopWorking = allCrew[a].GetComponent<NavMeshAgent>();
            if (stopWorking.destination != null)
            {
                StartCoroutine(pauseWork(stopWorking));
            }
        }
    }



    #endregion

    #region Storage Room
    public void GoodExtra()
    {
        for(int i = 0; i < 3; i++)
        {
            AddResoruce(randomResrouceSelection(), 3);
        }


    }
    public void BadJenga()
    {
        for (int i = 0; i < 5; i++)
        {
            RemoveResoruce(randomResrouceSelection(), 2);
        }

    }
    public void GoodPacker()
    {
        int a = Random.Range(0, allCrew.Count);
        NavMeshAgent boost = allCrew[a].GetComponent<NavMeshAgent>();
        speedBoost(boost, 10);
    }
    #endregion

    #region Water Room
    public void GoodSlide()
    {
        foreach (var crew in allCrew)
        {
            prodBoost(0.2f, 25);
        }

    }
    public void BadGrylls(bool playerchoice)
    {
        if (playerchoice)
        {
            RemoveResoruce("medicalsupplies", 25);
            int a = Random.Range(0, allCrew.Count);
            NavMeshAgent boost = allCrew[a].GetComponent<NavMeshAgent>();
            StartCoroutine(pauseWork(boost));
        }
        else removeRandomCrew(1);
    }
    public void BadPowder(bool playerchoice)
    {
        if (playerchoice) RemoveResoruce("Water", 2);
        else removeRandomCrew(1); 
    }

    #endregion


    #endregion

    #region GeneralEventActions
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
                ResourceManager.Instance.food -= amount * foodCostMulti;
                WeightManager.Instance.removeResourceWeight(amount * foodCostMulti, 1);
                ResourceManager.Instance.detectedInvenToRemove(amount * foodCostMulti, 0);
                break;
            case "water":
                ResourceManager.Instance.water -= amount * waterCostMulti;
                WeightManager.Instance.removeResourceWeight(amount * waterCostMulti, 1);
                ResourceManager.Instance.detectedInvenToRemove(amount * waterCostMulti, 1);
                break;
            case "medicalsupplies":
                ResourceManager.Instance.medicalSupplies -= amount * medicalCostMultiplier;
                WeightManager.Instance.removeResourceWeight(amount * medicalCostMultiplier, 3);
                ResourceManager.Instance.detectedInvenToRemove(amount * medicalCostMultiplier, 2);
                break;
            case "ammo":
                ResourceManager.Instance.ammo -= amount * ammoCostMulti;
                WeightManager.Instance.removeResourceWeight(amount * ammoCostMulti, 2);
                ResourceManager.Instance.detectedInvenToRemove(amount * ammoCostMulti, 3);
                break;
            case "fuel":
                ResourceManager.Instance.fuel -= amount * fuelCostMulti;
                WeightManager.Instance.removeResourceWeight(amount * fuelCostMulti, 2);
                ResourceManager.Instance.detectedInvenToRemove(amount * fuelCostMulti, 4);
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
                WeightManager.Instance.addResourceWeight(amount, 1);
                ResourceManager.Instance.detectedInvenToadd(amount, 0);
                break;
            case "water":
                ResourceManager.Instance.water += amount;
                WeightManager.Instance.addResourceWeight(amount, 1);
                ResourceManager.Instance.detectedInvenToadd(amount, 1);
                break;
            case "medicalsupplies":
                ResourceManager.Instance.medicalSupplies += amount;
                WeightManager.Instance.addResourceWeight(amount, 3);
                ResourceManager.Instance.detectedInvenToadd(amount, 2);
                break;
            case "ammo":
                ResourceManager.Instance.ammo += amount;
                WeightManager.Instance.addResourceWeight(amount, 2);
                ResourceManager.Instance.detectedInvenToadd(amount, 3);
                break;
            case "fuel":
                ResourceManager.Instance.fuel += amount;
                WeightManager.Instance.addResourceWeight(amount, 2);
                ResourceManager.Instance.detectedInvenToadd(amount, 4);
                break;
            default:
                break;
        }
    }

    public void randomDestination()
    {
        Vector3 randomPos = new Vector3(Random.Range(0, 20), 0f, Random.Range(0, 20));

        int a = Random.Range(0, allCrew.Count);
        NavMeshAgent boost = allCrew[a].GetComponent<NavMeshAgent>();
       if(boost.destination != null)
        {
            boost.destination = randomPos;
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
    IEnumerator speedDebuff(NavMeshAgent target, int boostTime)
    {
        float startSpeed = target.speed;

        target.speed /= 1.5f;

        yield return new WaitForSeconds(boostTime);
        target.speed = startSpeed;
    }

    IEnumerator prodBoost(float prodBoost, int boostTime)
    {
      
        gameObject.GetComponent<ResourceGen>().crewMultiplier = 1 - (prodBoost * allCrew.Count);

        yield return new WaitForSeconds(boostTime);
        gameObject.GetComponent<ResourceGen>().crewMultiplier = 1 - (0.1f * allCrew.Count);
    }
    public string randomResrouceSelection()
    {
        int rand = Random.Range(0, 6);
        List<string> tempList = new List<string>();
        tempList.Add("food");
        tempList.Add("ammo");
        tempList.Add("food");
        tempList.Add("fuel");
        tempList.Add("water");
        string selection = tempList.ElementAt(rand);
        
        return selection;
    }
    
    public void removeRandomRoom(int toRemove)
    {
        for (int i = 0; i <= toRemove; i++)
        {
            int rand = Random.Range(0, WeightManager.Instance.rooms.Count + 1);

            WeightManager.Instance.rooms.ElementAt(rand).RoomDetached();
            
        }
    }
 /*public int getHigherRemovalValue(int totalResources, int removal)
    {
        int calculatedRemoval = totalResources > 750 ? totalResources / 10 : removal;
        return Mathf.Max(removal, calculatedRemoval);
    }*/

    

    #endregion

    //return % of remaining rooms for post game evaluation
    public float GetPercentage()
    {
        int currRoomCount = FindObjectsOfType<Room>().Length;
        if (startingRoomCount == 0) return 0; 
        return ((float)currRoomCount / startingRoomCount) * 100;
    } 
}