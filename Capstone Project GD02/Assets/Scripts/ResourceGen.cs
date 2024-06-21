using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGen : MonoBehaviour
{
    [Header("Wait Times")]
    public float foodWaitTime;
    public float waterWaitTime;
    public float medicalWaitTime;
    public float ammoWaitTime;
    public float fuelWaitTime;

    [Header("Increase Amount")]
    public int foodIncrease;
    public int waterIncrease;
    public int medicalIncrease;
    public int ammoIncrease;
    public int fuelIncrease;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Contains("Food"))
        {
            StartCoroutine(Food());
        }

        if (gameObject.name.Contains("Water"))
        {
            StartCoroutine(Water());
        }

        if (gameObject.name.Contains("Medical"))
        {
            StartCoroutine(Medical());
        }

        if (gameObject.name.Contains("Ammo"))
        {
            StartCoroutine(Ammo());
        }

        if (gameObject.name.Contains("Fuel"))
        {
            StartCoroutine(Fuel());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Food()
    {
        yield return new WaitForSeconds(foodWaitTime);
        ResourceManager.Instance.food += foodIncrease;
<<<<<<< Updated upstream
       // Debug.Log(ResourceManager.Instance.food + "Food");
=======
        gameObject.GetComponent<IndividualInventoryScript>().inventory[0] += foodIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        WeightManager.Instance.addResourceWeight(foodIncrease, WeightManager.Instance.foodWeight);
>>>>>>> Stashed changes
        StartCoroutine(Food());
    }

    IEnumerator Water()
    {
        yield return new WaitForSeconds(waterWaitTime);
        ResourceManager.Instance.water += waterIncrease;
<<<<<<< Updated upstream
        // Debug.Log(ResourceManager.Instance.water + "Water");
=======
        gameObject.GetComponent<IndividualInventoryScript>().inventory[1] += waterIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        WeightManager.Instance.addResourceWeight(waterIncrease, WeightManager.Instance.waterWeight);
>>>>>>> Stashed changes
        StartCoroutine(Water());
    }

    IEnumerator Medical()
    {
        yield return new WaitForSeconds(medicalWaitTime);
        ResourceManager.Instance.medicalSupplies += medicalIncrease;
<<<<<<< Updated upstream
       // Debug.Log(ResourceManager.Instance.medicalSupplies + " Medical");
=======
        gameObject.GetComponent<IndividualInventoryScript>().inventory[2] += medicalIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        WeightManager.Instance.addResourceWeight(medicalIncrease, WeightManager.Instance.medicalWeight);
>>>>>>> Stashed changes
        StartCoroutine(Medical());
    }

    IEnumerator Ammo()
    {
        yield return new WaitForSeconds(ammoWaitTime);
        ResourceManager.Instance.ammo += ammoIncrease;
<<<<<<< Updated upstream
        //Debug.Log(ResourceManager.Instance.ammo + "Ammo");
=======
        gameObject.GetComponent<IndividualInventoryScript>().inventory[3] += ammoIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        WeightManager.Instance.addResourceWeight(ammoIncrease, WeightManager.Instance.ammoWeight);
>>>>>>> Stashed changes
        StartCoroutine(Ammo());
    }

    IEnumerator Fuel()
    {
        yield return new WaitForSeconds(fuelWaitTime);
        ResourceManager.Instance.fuel += fuelIncrease;
<<<<<<< Updated upstream
        //Debug.Log(ResourceManager.Instance.fuel + "Fuel");
=======
        gameObject.GetComponent<IndividualInventoryScript>().inventory[4] += fuelIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        WeightManager.Instance.addResourceWeight(fuelIncrease, WeightManager.Instance.fuelWeight);
>>>>>>> Stashed changes
        StartCoroutine(Fuel());
    }
}
