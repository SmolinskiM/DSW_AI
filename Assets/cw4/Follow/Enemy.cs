using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(player.position, transform.position) <= 2)
        {
            agent.stoppingDistance = 3;
        }
        else
        {
            agent.stoppingDistance = 0;
        }
    }
}
