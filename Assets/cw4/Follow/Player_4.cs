using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_4 : MonoBehaviour
{
    private float speed = 5;

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }
}
