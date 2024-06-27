using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;


    [SerializeField] List<GameObject> cellList;
    [SerializeField] GameObject cratePrefab;

    public bool inBuildMode = false;
    public Vector3 spawnCell;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        cellList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cell"));
        SetCells(false);
    }

    void Update()
    {
        
        if (inBuildMode) { spawnCell = ClosestCell(); }
        // Enter/ exit build mode on B key
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildMode();
        }
       
        //Vector3 spawnPos = ClosestCell();
        if(inBuildMode && Input.GetMouseButtonDown(0))
        {
            Instantiate(cratePrefab, spawnCell, Quaternion.identity);
            inBuildMode = false;
        }

        if (inBuildMode)
        {
            SetCells(true);
        }
        else
        {
            SetCells(false);
        }

    }

    public Vector3 ClosestCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 9999f))
        {
            GameObject closestCell = null;
            float closestDistanceSqr = float.MaxValue;

            foreach (GameObject cell in cellList)
            {
                float distanceSqr = (cell.transform.position - hit.point).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestCell = cell;
                    closestDistanceSqr = distanceSqr;
                }
            }

            return closestCell.transform.position;
        }

        return Vector3.zero;
    }

    void SetCells(bool isActive)
    {

        foreach (GameObject cell in cellList)
        {
           cell.SetActive(isActive);
        }
    }
    public void ToggleBuildMode()
    {
        inBuildMode = !inBuildMode;
    }

}
