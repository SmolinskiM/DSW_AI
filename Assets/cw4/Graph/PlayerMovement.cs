using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public WayFinder wayFinder;

    private NavMeshAgent agent;

    public GameObject destination;

    public List<GameObject> trace = new List<GameObject>();

    private int i = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        if(i >=  trace.Count)
        {
            return;
        }

        if(agent.remainingDistance == 0)
        {

            agent.SetDestination(trace[i].transform.position);
            i++;
        }
    }

    public void SetPath(GameObject destination)
    {
        i = 0;
        trace = wayFinder.CalculationTrace(destination);
        agent.SetDestination(trace[i].transform.position);
        i++;
    }
}
