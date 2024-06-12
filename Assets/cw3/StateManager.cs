using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager 
{
    public enum State
    {
        Patrolling,
        Alarmed,
        Chase,
        Attack
    }

    public NavMeshAgent agent;
    public Transform[] wayPoints;
    public Transform player;
    public Material material;
    public Transform enemy;
    private int wayPointIndex = 0;

    private float timeAlarmed;

    private float attackDistance = 2;

    private float triggerPatrollingDistance = 5;
    private float triggerAlarmedDistance = 10;

    private Color patrollingColor = Color.green;
    private Color allarmedColor = Color.yellow;
    private Color chaseColor = new Color(1, (float)165 / 255, 0);
    private Color attackColor = Color.red;

    private State state;

    public void Procces()
    {
        switch(state)
        {
            case(State.Patrolling):
                Patrolling();
                break;

            case(State.Alarmed):
                Alarmed();
                break;

            case(State.Chase):
                Chase();
                break;

            case(State.Attack):
                Attack();
                break;
        }
    }

    public StateManager(NavMeshAgent agent, Transform[] wayPoints, Transform player, Material material, Transform enemy)
    {
        this.agent = agent;
        this.wayPoints = wayPoints;
        this.player = player;
        this.material = material;
        this.enemy = enemy;
    }

    public void Patrolling() 
    {
        material.color = patrollingColor;

        agent.SetDestination(wayPoints[wayPointIndex / wayPoints.Length].position);

        Debug.Log(agent.remainingDistance);

        if(agent.remainingDistance == 0)
        {
            wayPointIndex++;
        }

        if (Vector3.Distance(enemy.position, player.position) <= triggerPatrollingDistance)
        {
            state = State.Chase;
        }
    }

    public void Alarmed()
    {
        material.color = allarmedColor;
        timeAlarmed += Time.deltaTime;
        agent.stoppingDistance = 0;
        agent.SetDestination(wayPoints[wayPointIndex / wayPoints.Length].position);

        if (agent.remainingDistance == 0)
        {
            wayPointIndex++;
        }

        if(timeAlarmed > 5)
        {
            timeAlarmed = 0;
            state = State.Patrolling;
        }

        if (Vector3.Distance(enemy.position, player.position) <= triggerAlarmedDistance)
        {
            timeAlarmed = 0;
            state = State.Chase;
        }
    }

    public void Chase()
    {
        material.color = chaseColor;
        agent.SetDestination(player.position);
        agent.stoppingDistance = 2;

        if (Vector3.Distance(enemy.position, player.position) > triggerAlarmedDistance)
        {
            state = State.Alarmed;
        }

        if(Vector3.Distance(enemy.position, player.position) <= attackDistance)
        {
            state = State.Attack;
        }
    }

    public void Attack()
    {
        material.color = attackColor;

        if (Vector3.Distance(enemy.position, player.position) > attackDistance)
        {
            state = State.Chase;
        }
    }
}
