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
       // Debug.Log(ResourceManager.Instance.food + "Food");
        StartCoroutine(Food());
    }

    IEnumerator Water()
    {
        yield return new WaitForSeconds(waterWaitTime);
        ResourceManager.Instance.water += waterIncrease;
        // Debug.Log(ResourceManager.Instance.water + "Water");
        StartCoroutine(Water());
    }

    IEnumerator Medical()
    {
        yield return new WaitForSeconds(medicalWaitTime);
        ResourceManager.Instance.medicalSupplies += medicalIncrease;
       // Debug.Log(ResourceManager.Instance.medicalSupplies + " Medical");
        StartCoroutine(Medical());
    }

    IEnumerator Ammo()
    {
        yield return new WaitForSeconds(ammoWaitTime);
        ResourceManager.Instance.ammo += ammoIncrease;
        //Debug.Log(ResourceManager.Instance.ammo + "Ammo");
        StartCoroutine(Ammo());
    }

    IEnumerator Fuel()
    {
        yield return new WaitForSeconds(fuelWaitTime);
        ResourceManager.Instance.fuel += fuelIncrease;
        //Debug.Log(ResourceManager.Instance.fuel + "Fuel");
        StartCoroutine(Fuel());
    }
}
