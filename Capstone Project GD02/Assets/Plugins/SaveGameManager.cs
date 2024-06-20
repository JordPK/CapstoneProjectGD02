using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class SaveGameManager : MonoBehaviour
{
    
    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SaveBool(string filename, string varName, bool value);
    
    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SaveFloat(string filename, string varName, float value);

    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SaveInt(string filename, string varName, int value);

    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SaveString(string filename, string varName, string value);

    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SaveVector3(string filename, string varName, float x, float y, float z);


    //LOADING FUNCTION DEF
    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LoadBoolValue(string filename, string varName);

    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern float LoadFloatValue(string filename, string varName);

    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int LoadIntValue(string filename, string varName);

    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr LoadStringValue(string filename, string varName);

    [DllImport("SaveDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void LoadVector3Values(string filename, string varName, out float x, out float y, out float z);



    void Start()
    {
        // Example usage: 
        // Typically, you will be using SaveGameManager.<insert one of the lines below> 
        //SaveGameManager.SaveFloat("CameraSettings.txt", "FPSCamSensitivity", FPSCamSensitivity);
        SaveBool("XroomEjected.txt", "roomEjected", true);
        SaveFloat("camSettings.txt", "FPSCameraSens", 6.2f);
        SaveInt("crewCap.txt", "maxCrew", 2);
        SaveString("name.txt", "playerName", "john doe");
        SaveVector3("pos.txt", "playerPosition", transform.position.x, transform.position.y, transform.position.z); 
           
    }
}
