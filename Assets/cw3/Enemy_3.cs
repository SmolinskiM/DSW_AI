using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_3 : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoint;
    [SerializeField] private Transform player;
    [SerializeField] private Material material;

    private StateManager stateManager;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateManager = new StateManager(agent, wayPoint, player, material, transform);
    }

    private void Update()
    {
        stateManager.Procces();
    }
}
