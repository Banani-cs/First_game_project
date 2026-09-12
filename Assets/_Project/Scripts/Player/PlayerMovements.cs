using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public CharacterController controller;

    [field: SerializeField] private float speed = 12f;
    [field: SerializeField] private float gravity = -9.81f * 2;
    [field: SerializeField] private float jumpHeight = 3f;

    [field: SerializeField] private Transform groundCheck;
    [field: SerializeField] private float groundDistance = 0.4f;
    [field: SerializeField] private LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //add a small negative value to the velocity, so we wouldnt be floating every milisecond, just like how we have gravity to apply a small force to the ground to keep us on the ground in real life(adding weights basically)
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //right is the red Axis, foward is the blue axis
        //We dont use vector.forward or vector.right, because vector is the direction of the entire world, transform is the direction of the player.
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            //Multiple by -2f becausse gravity is negative, and we want to make it positive, and the 2f is just a constant that makes the jump feel more natural
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}