using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCarController : MonoBehaviour
{
    public int checkPoints = 0;

    public AudioClip crashSound;

    public AudioClip checkpoint;

    private AudioSource playerSound;

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 40f;
    public float motorForce = 150f;
    public float brakeForce = 0f;

    private float currentbrakeForce;



    void Awake(){
        ResetAllCheckpoints();
        playerSound = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbrakeForce = isBreaking ? brakeForce : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbrakeForce;
        frontLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearRightWheelCollider.brakeTorque = currentbrakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

 

    
    private void OnCollisionEnter(Collision other) {
        
        if(other.transform.tag == "Ground"){
            
            GameManager.Instance.ResetVehicle();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Checkpoint"){
            if(other.GetComponent<CheckpointChecker>().getPassed() == false){
                other.GetComponent<CheckpointChecker>().setPassed();
                checkPoints--;
                playerSound.PlayOneShot(checkpoint);
            }
        }

        if(other.transform.tag == "Start"){
            if(checkPoints == 0) {
                GameManager.Instance.stopPlaying();
                GameManager.Instance.win();
            }
        }
    }

    void ResetAllCheckpoints() {
        GameObject[] cPoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        for(int i = 0; i < cPoints.Length; i++) {
            cPoints[i].GetComponent<CheckpointChecker>().setPassed(false);
        }
        checkPoints = cPoints.Length;
    }

    public void playCrash(){
        playerSound.PlayOneShot(crashSound);
    }
}
