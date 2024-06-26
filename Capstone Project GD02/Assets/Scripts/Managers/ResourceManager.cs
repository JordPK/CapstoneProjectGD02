using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int food = 0;
    public int water = 0;
    public int medicalSupplies = 0;
    public int ammo = 0;
    public int fuel = 0;

    public static ResourceManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void detectedInvenToRemove(int toRemove, int resrouceIndex)
    {
        int removedResrouces = 0;
        IndividualInventoryScript[] allInventories = FindObjectsOfType<IndividualInventoryScript>();
        foreach (IndividualInventoryScript inventory in allInventories)
        {
            if(removedResrouces >= toRemove)  break;
            if(toRemove > 0)
            {
                Debug.Log("inside to remove > 0 ");
               if(toRemove > inventory.inventory[resrouceIndex])
                {
                    Debug.Log("to remove > inven index remove remaining inventory");
                    inventory.inventory[resrouceIndex] -= inventory.inventory[resrouceIndex];
                    removedResrouces += inventory.inventory[resrouceIndex];
                    toRemove -= inventory.inventory[resrouceIndex];
                }
                else
                {
                    Debug.Log("to remove removed all from inven");
                    inventory.inventory[resrouceIndex] -= toRemove;
                    removedResrouces += toRemove;
                }

            }

        }

    }

    public void detectedInvenToadd(int toAdd, int resrouceIndex)
    {
        IndividualInventoryScript[] firstStorageRoom = FindObjectsOfType<IndividualInventoryScript>(); //.GetConponet<IndividualInventoryScript>();
        foreach(IndividualInventoryScript inventory in firstStorageRoom)
        {
            if (inventory.gameObject.name.Contains("Storage"))
            {
                inventory.inventory[resrouceIndex] += toAdd;
                Debug.Log("Added to " +  inventory.gameObject.name );
                break;
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

   
}
