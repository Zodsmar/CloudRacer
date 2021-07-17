using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 4.0f;
    private float turnSpeed = 75.0f;
    private float horizontalInput;
    private float verticalInput;

    public int checkPoints = 0;

    public AudioClip crashSound;

    public AudioClip checkpoint;

    private AudioSource playerSound;
    private Rigidbody rigidbody;

    void Awake(){
        ResetAllCheckpoints();
        playerSound = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            //Get controls
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            //Move the vehicle forward
            //rigidbody.AddForce(Vector3.right * Time.deltaTime * speed * verticalInput, ForceMode.Impulse);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            //Rotate the vehicle instead of slide around
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            if(Input.GetKeyDown(KeyCode.Escape)){
                GameManager.Instance.stopPlaying();
            }
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
