using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        float x = 0; 
        float z = 0;

        if (Input.GetKey("a"))
        {
            x += -1;
        }

        if (Input.GetKey("d"))
        {
            x += 1;
        }
        if (Input.GetKey("w"))

            z += 1;

        if (Input.GetKey("s"))

            z += -1;


            Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity);


    }
}
