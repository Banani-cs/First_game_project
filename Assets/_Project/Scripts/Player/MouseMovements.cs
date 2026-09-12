using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovements : MonoBehaviour
{

    [field: SerializeField] private float mouseSensitivity = 100f;
    [field: SerializeField] private Transform playerBody;

    float xRotation = 0f;
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Getting the mouse input//The Time.deltaTime is used to prevent the mouse from being too sensitive and to make it frame rate independent

        //Without the Time.deltaTime, a person with a higher FPS will spin faster than the one that has a lower  FPS. But to be fair the GetAxis function is already frame independent, so we can skip the Time.deltaTime, but we will keep it for now, just in case we want to change the sensitivity later on. Just a note
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //control rotation around x axis (Look up and down)

        //The reason that we subtract the mouseY is because Unity does the rotation in the opposite direction of what we want. So we need to invert it

        //Take your head as an example:

        //When u move your heads up, your head goes down in Unity, and when u move your head down, your head goes up in Unity. So we gotta invert it to make it work properly.
        xRotation -= mouseY;

        //we clamp the rotation so we cant Over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //control rotation around y axis (Look up and down)
        //applying both rotations

        //You can see theres 3 parameters (X,Y,Z), the reason Z is locked to 0 is that we dont  want to rotate around it. Take your neck as an example, moving your neck to the side is basically rotating around the Z axis, which in this game, we dont want to do that

        // CAN WORK IF WERE DOING SOMETHING THAT REQUIRES LEANING.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

    }
}