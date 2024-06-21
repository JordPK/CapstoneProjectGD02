using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeightManager : MonoBehaviour
{
    public int totalWeight;
    public int totalEjectedWeight;
    
    public int targetWeightLoss;

    public List<Room> rooms = new List<Room>();

    [Header("Resouce Weights")]
    public int foodWeight = 1;
    public int waterWeight = 1;
    public int medicalWeight = 3;
    public int ammoWeight = 2;
    public int fuelWeight = 2;

    public static WeightManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
        rooms = FindObjectsOfType<Room>().ToList<Room>();
        foreach (Room room in rooms)
        {
            //-------------------- UNCOMMENT WHEN ALL IN  MAIN -----------------
            totalWeight += room.roomWeight;
            Debug.Log(totalWeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    public bool metTargetWeightLoss()
    {
        return totalEjectedWeight <= targetWeightLoss;
    }


    public int generateDayWeightLossTarget()
    {
        return Random.Range(1, totalWeight);
     
    }


    private void updateEjectedWeight(int Weight)
    {
        totalEjectedWeight += Weight;
    }
    public void reduceWeight(int Weight)
    {
        totalWeight -= Weight;
        updateEjectedWeight(Weight);
    }
    public void addWeight(int Weight)
    {
        totalWeight += Weight;
    }

    public void addResourceWeight(int resoruceCount, float resrouceBaseWeight)
    {
        totalWeight += Mathf.FloorToInt(resoruceCount* resrouceBaseWeight) ;
    }  
    public void removeResourceWeight(int resoruceCount, float resrouceBaseWeight)
    {
        int reduceammount = Mathf.FloorToInt(resoruceCount * resrouceBaseWeight);
        totalWeight -= reduceammount;
        updateEjectedWeight(reduceammount);
    }

}
