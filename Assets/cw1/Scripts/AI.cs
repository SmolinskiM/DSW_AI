using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRotation;
    
    [SerializeField] private GameObject player;
    [SerializeField] private MoveShell bullet;
    [SerializeField] private GameObject turret;

    private float cooldown = 1.5f;

    private void Update()
    {
        Movement();
        Rotation();

        if(Time.time < cooldown)
        {
            return;
        }
        Attack();
        cooldown = Time.time + 1.5f;
    }

    private void Movement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void Rotation()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        float dot = Vector3.Dot(Vector3.forward, direction);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        Vector3 axis = Vector3.Cross(Vector3.forward, direction).normalized;

        Quaternion lookRotation = Quaternion.AngleAxis(angle, axis);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation);
    }

    private void Attack()
    {
        MoveShell shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        float distance = Vector3.Distance(player.transform.position, transform.position);

        shell.speed = distance;
    }
}
