using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrewManager : MonoBehaviour
{
    public GameObject selection;
    private NavMeshAgent _agent;
    private Renderer _renderer;

    public static CrewManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);

        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            moveUnit();     
        }

    public void selectUnit(Transform unit)
    {
      _agent = unit.GetComponent<NavMeshAgent>();
        selection = unit.gameObject;

    }

    void moveUnit()
    {
        if (Input.GetMouseButtonDown(0) && _agent != null && selection.GetComponent<agent>().isMouseOver == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
                selection.GetComponent<agent>().UnselectUnit();
                _agent = null;
               
            }

        }
    }
    
}