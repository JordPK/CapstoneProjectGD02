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

    public float crewMultiplier = 1;


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

        if (gameObject.name.Contains("Medbay"))
        {
            StartCoroutine(Medical());
        }

        if (gameObject.name.Contains("Ammo"))
        {
            StartCoroutine(Ammo());
        }

        if (gameObject.name.Contains("Generator"))
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
        yield return new WaitForSeconds(foodWaitTime * crewMultiplier);
        ResourceManager.Instance.food += foodIncrease;
        gameObject.GetComponent<IndividualInventoryScript>().inventory[0] += foodIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        //WeightManager.Instance.addResourceWeight(foodIncrease, WeightManager.Instance.foodWeight);
        StartCoroutine(Food());
    }

    IEnumerator Water()
    {
        yield return new WaitForSeconds(waterWaitTime * crewMultiplier);
        ResourceManager.Instance.water += waterIncrease;
        gameObject.GetComponent<IndividualInventoryScript>().inventory[1] += waterIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        //WeightManager.Instance.addResourceWeight(waterIncrease, WeightManager.Instance.waterWeight);
        StartCoroutine(Water());
    }

    IEnumerator Medical()
    {
        yield return new WaitForSeconds(medicalWaitTime * crewMultiplier);
        ResourceManager.Instance.medicalSupplies += medicalIncrease;
        gameObject.GetComponent<IndividualInventoryScript>().inventory[2] += medicalIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        //WeightManager.Instance.addResourceWeight(medicalIncrease, WeightManager.Instance.medicalWeight);
        StartCoroutine(Medical());
    }

    IEnumerator Ammo()
    {
        yield return new WaitForSeconds(ammoWaitTime * crewMultiplier);
        ResourceManager.Instance.ammo += ammoIncrease;
        gameObject.GetComponent<IndividualInventoryScript>().inventory[3] += ammoIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        //WeightManager.Instance.addResourceWeight(ammoIncrease, WeightManager.Instance.ammoWeight);
        StartCoroutine(Ammo());
    }

    IEnumerator Fuel()
    {
        yield return new WaitForSeconds(fuelWaitTime * crewMultiplier);
        ResourceManager.Instance.fuel += fuelIncrease;
        gameObject.GetComponent<IndividualInventoryScript>().inventory[4] += fuelIncrease;
        //-------------------UNCOMMENT WHEN INTEGRATED-------------------
        //WeightManager.Instance.addResourceWeight(fuelIncrease, WeightManager.Instance.fuelWeight);
        StartCoroutine(Fuel());
    }
}
