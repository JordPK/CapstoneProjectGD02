using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Resource Info", fileName = "New Resource Info")]
public class resourceInfo : ScriptableObject
{
    public string resourceName;
    public Sprite resourceImage;
    public int resourceWeight;
}
