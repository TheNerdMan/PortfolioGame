using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float CameraMoveSpeed = 120f;
    public GameObject CameraFollowObject;
    Vector3 FollowPOS;
    public float ClampAngle = 80f; 
    public float InputSens = 150f; 
    public GameObject CameraObject; 
    public GameObject PlayerObject; 
    public float CamDistanceXToPlayer; 
    public float CamDistanceYToPlayer; 
    public float CamDistanceZToPlayer; 
    public float mouseX; 
    public float mouseY; 
    public float finalInputX; 
    public float finalInputZ; 
    public float smoothX; 
    public float smoothY; 
    private float rotationY = 0f; 
    private float rotationX = 0f; 
     

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        //Roate Same as ship
        rotationY = rotation.y;
        rotationX = rotation.x;

    }

    // Update is called once per frame
    void Update()
    {

        //rotationX = Mathf.Clamp(rotationX, -ClampAngle, ClampAngle);
        rotationY = PlayerObject.transform.rotation.y;
        rotationX = PlayerObject.transform.rotation.x;


        //Quaternion localRotaion = Quaternion.Euler(rotationX, rotationY, 0.0f);
        transform.rotation = PlayerObject.transform.rotation;
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = CameraFollowObject.transform;

        //move towards GO target
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
