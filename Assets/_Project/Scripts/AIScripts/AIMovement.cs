using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{

    private Animator _animator;

    private Rigidbody _rigidBody;
    [SerializeField] private float _moveSpeed = 2f;
    private float _walkTime;
    private float _walkCounter;
    private float _waitTime;
    private float _waitCounter;

    private MovementDirection _walkDirection;

    private bool _isWalking;

    public enum MovementDirection
    {
        North,
        East,
        South,
        West

    }
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();

        _rigidBody = GetComponent<Rigidbody>();

        //So that all the prefabs don't move/stop at the same time
        _walkTime = Random.Range(3, 9);
        _waitTime = Random.Range(5, 10);


        _waitCounter = _waitTime;
        _walkCounter = _walkTime;

        ChooseDirection();
    }
    private void Update()
    {
        if (_isWalking)
        {

            _animator.SetBool("isRunning", true);

            _walkCounter -= Time.deltaTime;
            //Chooses a random direction to move.
            /*switch (WalkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
            }*/    //Moved to FixedUpdate() because it is more optimized to do physics in FixedUpdate() rather than Update()
            if (_walkCounter <= 0)
            {
                _isWalking = false;
                //stop movement
                _animator.SetBool("isRunning", false);
                //reset the waitCounter
                _waitCounter = _waitTime;
            }


        }
        else
        {

            _waitCounter -= Time.deltaTime;

            if (_waitCounter <= 0)
            {
                //Continue moving after the wait time is over
                ChooseDirection();
            }
        }
    }

    private void FixedUpdate()
    {
        //Chooses a random direction to move.
        //Using RigidBody to move so physics can be applied to the AI, like colliding with walls and other objects. This is a much better practice than spamming transform.position, because that will just teleport the AI to the new position, and it will not collide with anything, which is not what we want.
        if (_isWalking)
        {
            switch (_walkDirection)
            {
                case MovementDirection.North:
                    _rigidBody.MoveRotation(Quaternion.Euler(0f, 0f, 0f));
                    _rigidBody.linearVelocity = new Vector3(transform.forward.x * _moveSpeed, _rigidBody.linearVelocity.y, transform.forward.z * _moveSpeed);
                    break;
                case MovementDirection.East:
                    _rigidBody.MoveRotation(Quaternion.Euler(0f, 90, 0f));
                    _rigidBody.linearVelocity = new Vector3(transform.forward.x * _moveSpeed, _rigidBody.linearVelocity.y, transform.forward.z * _moveSpeed);
                    break;
                case MovementDirection.West:
                    _rigidBody.MoveRotation(Quaternion.Euler(0f, -90, 0f));
                    _rigidBody.linearVelocity = new Vector3(transform.forward.x * _moveSpeed, _rigidBody.linearVelocity.y, transform.forward.z * _moveSpeed);
                    break;
                case MovementDirection.South:
                    _rigidBody.MoveRotation(Quaternion.Euler(0f, 180, 0f));
                    _rigidBody.linearVelocity = new Vector3(transform.forward.x * _moveSpeed, _rigidBody.linearVelocity.y, transform.forward.z * _moveSpeed);
                    break;
            }
        }
        else
        {
            _rigidBody.linearVelocity = new Vector3(0f, _rigidBody.linearVelocity.y, 0f);
        }
    }

    //Method to move.
    public void ChooseDirection()
    {
        _walkDirection = (MovementDirection)Random.Range(0, 4);

        _isWalking = true;
        _walkCounter = _walkTime;
    }
}