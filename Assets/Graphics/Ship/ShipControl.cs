using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{

    public float ShipSpeed = 1f;
    public float SpeedStep = 0.5f;
    public float MaxSpeed = 10f;
    public float ClampAngle = 80f;
    public float InputSens = 150f;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotationY = 0f;
    private float rotationX = 0f;

    public CharacterController CharacterController;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationY = rotation.y;
        rotationX = rotation.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //We point ship in the correct direction
    void Update()
    {
        //Rotate Ship's "Nose" by users mouse/controller movments  
        ChangeShipFoward();
        ChangeShipSpeed();
    }

    //Then we move the ship in that new direction 1 step (step defined by users clicks)
    void LateUpdate()
    {
        //Move a single step
        MoveShip();
    }

    private void ChangeShipFoward()
    {
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotationY += finalInputX * InputSens * Time.deltaTime;
        rotationX += finalInputZ * InputSens * Time.deltaTime;

        //rotationY = Mathf.Clamp(rotationY, -ClampAngle, ClampAngle);
        rotationX = Mathf.Clamp(rotationX, -ClampAngle, ClampAngle);

        Quaternion localRotaion = Quaternion.Euler(rotationX, rotationY, 0.0f);
        transform.rotation = localRotaion;
    }   

    void MoveShip()
    {
        //find step value
        float step = ShipSpeed * 5;

        //because we are attached to the ship object, transform.transform is the ships "nose"
        Vector3 newLocation = (transform.forward * step);

        //Move forward one step
        //transform.position = Vector3.MoveTowards(transform.position, newLocation, step);
        CharacterController.Move(newLocation * ShipSpeed * Time.deltaTime);
    }


    private bool increaseSpeed = false; 
    private bool decreaseSpeed = false; 
    private void ChangeShipSpeed()
    {

        if (Input.GetMouseButtonDown(0))
            increaseSpeed = true;

        if (Input.GetMouseButtonDown(1))
            decreaseSpeed = true;
               
        if (Input.GetMouseButtonUp(0))
            increaseSpeed = false;

        if (Input.GetMouseButtonUp(1))
            decreaseSpeed = false;

        if (increaseSpeed)
            ShipSpeed += SpeedStep;

        if (decreaseSpeed)
            ShipSpeed -= SpeedStep;

        ShipSpeed = Mathf.Clamp(ShipSpeed, 1f, MaxSpeed);
    }

}
