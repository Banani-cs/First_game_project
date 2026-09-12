using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _gravity = -9.81f * 2;
    [SerializeField] private float _jumpHeight = 3f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _velocity;

    private bool _isGrounded;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        //add a small negative value to the velocity, so we wouldnt be floating every milisecond, just like how we have gravity to apply a small force to the ground to keep us on the ground in real life(adding weights basically)
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //right is the red Axis, foward is the blue axis
        //We dont use vector.forward or vector.right, because vector is the direction of the entire world, transform is the direction of the player.
        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);

        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            //the equation for jumping
            //Multiple by -2f becausse gravity is negative, and we want to make it positive, and the 2f is just a constant that makes the jump feel more natural
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}