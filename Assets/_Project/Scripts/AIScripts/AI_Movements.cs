using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movements : MonoBehaviour
{

    Animator animator;

    private Rigidbody rigidBody;
    [field: SerializeField] private float moveSpeed = 2f;
    float walkTime;
    private float walkCounter;
    float waitTime;
    private float waitCounter;

    int walkDirection;

    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rigidBody = GetComponent<Rigidbody>();

        //So that all the prefabs don't move/stop at the same time
        walkTime = Random.Range(3, 9);
        waitTime = Random.Range(5, 10);


        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {

            animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;
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
            if (walkCounter <= 0)
            {
                isWalking = false;
                //stop movement
                animator.SetBool("isRunning", false);
                //reset the waitCounter
                waitCounter = waitTime;
            }


        }
        else
        {

            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                //Continue moving after the wait time is over
                ChooseDirection();
            }
        }
    }

    void FixedUpdate()
    {
        //Chooses a random direction to move.
        //Using RigidBody to move so physics can be applied to the AI, like colliding with walls and other objects. This is a much better practice than spamming transform.position, because that will just teleport the AI to the new position, and it will not collide with anything, which is not what we want.
        if (isWalking)
        {
            switch (walkDirection)
            {
                case 0:
                    rigidBody.MoveRotation(Quaternion.Euler(0f, 0f, 0f));
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
                case 1:
                    rigidBody.MoveRotation(Quaternion.Euler(0f, 90, 0f));
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
                case 2:
                    rigidBody.MoveRotation(Quaternion.Euler(0f, -90, 0f));
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
                case 3:
                    rigidBody.MoveRotation(Quaternion.Euler(0f, 180, 0f));
                    rigidBody.linearVelocity = new Vector3(transform.forward.x * moveSpeed, rigidBody.linearVelocity.y, transform.forward.z * moveSpeed);
                    break;
            }
        }
        else
        {
            rigidBody.linearVelocity = new Vector3(0f, rigidBody.linearVelocity.y, 0f);
        }
    }

    //Method to move.
    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;
    }
}