using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;


    [SerializeField] List<GameObject> cellList;
    [SerializeField] GameObject cratePrefab;

    public bool inBuildMode = false;


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
        // Enter/ exit build mode on B key
        if (Input.GetKeyDown(KeyCode.B) && !inBuildMode)
        {
            inBuildMode = true;

            Debug.Log("In Build Mode");
        }
        else if (inBuildMode && Input.GetKeyDown(KeyCode.B))
        {
            inBuildMode = false;
            Debug.Log("Not in Build mode");
        }
        //Vector3 spawnPos = ClosestCell();
        if(inBuildMode && Input.GetMouseButtonDown(0))
        {
            Instantiate(cratePrefab, ClosestCell(), Quaternion.identity);
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
        GameObject closestCell = null; 
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 9999f);

        float closestDistance = float.MaxValue; 

        foreach (GameObject cell in cells)
        {
            
            float distance = Vector3.Distance(hit.transform.position, cell.transform.position);
            if (distance < closestDistance)
            {
                closestCell = cell;
                closestDistance = distance;
                
            }
        }
           //closestCell.GetComponent<MeshRenderer>().material = 
            return closestCell.transform.position;
    }

    void SetCells(bool isActive)
    {

        foreach (GameObject cell in cellList)
        {
           cell.SetActive(isActive);
        }
    }


}
