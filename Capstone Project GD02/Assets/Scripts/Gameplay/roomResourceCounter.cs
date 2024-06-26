using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class roomResourceCounter : MonoBehaviour
{
    int[] resourceWeights = new int[5] { 1, 1, 3, 2, 2 };

    [SerializeField] TMP_Text roomResourceCounterText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(resourceCount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator resourceCount()
    {
        int[] tempInventory = gameObject.GetComponent<IndividualInventoryScript>().inventory;
        int totalWeight = 0;
        for (int i = 0; i < tempInventory.Length; i++)
        {
            totalWeight += tempInventory[i] * resourceWeights[i];
        }
        roomResourceCounterText.text = $"Total Cargo Weight: {totalWeight} kg";
        yield return new WaitForSeconds(2);
        StartCoroutine(resourceCount());
    }
}
