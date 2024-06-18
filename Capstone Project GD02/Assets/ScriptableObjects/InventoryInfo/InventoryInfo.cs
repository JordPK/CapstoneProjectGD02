using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Inventory Info", fileName = "New Inventory Info")]
public class InventoryInfo : ScriptableObject
{
    public int[] inventory = new int[5];
}
