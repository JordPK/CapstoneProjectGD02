using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomEvent", menuName = "Scriptable Objects/RoomEventData",order = 2)]
public class RoomEventData : ScriptableObject
{
    public List<string> goodEvents, badEvent = new List<string>(); 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
